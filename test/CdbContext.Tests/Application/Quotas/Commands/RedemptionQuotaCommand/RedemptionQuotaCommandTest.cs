using CdbContext.Application.Quotas.Commands.RedemptionQuotaCommand;
using CdbContext.Application.Quotas.Exceptions;
using CdbContext.DomainModels.InvestmentAccounts;
using CdbContext.DomainModels.Quotas;
using CdbContext.Infrastructure.Acls.JudicialBlock;
using CdbContext.Infrastructure.Acls.JudicialBlock.Responses;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CdbContext.Tests.Application.Quotas.Commands.RedemptionQuotaCommand
{
    public sealed class RedemptionQuotaCommandTest
    {
        //TODO: criar etapas dos testes abaixo
        //5. Garantir que a cota de compra foi atualizada/vendida
        //  - Chamar repositório para atualizar os dados cota e salvar a cota de venda
        //6. Movimentar da conta de investimentos para conta corrente
        //  - Utilizar uma ACL para realizar a movimentação
        //7. Se já foi consolidada e enviada para o parceiro, precisamos realizar o resgate no parceiro
        //  - Notificação para o módulo/contexto/microserviço/fila/tópico de integração(tópico no kafka ou uma fila no rabbit)

        private CancellationToken _cancellationToken;
        private Guid _investmentAccountIdValue;
        private InvestmentAccountId _investmentAccountId;
        private RedemptionQuotaCommandRequest _request;
        private RedemptionQuotaCommandHandler _handler;

        private Mock<IQuotaRepository> _quotaRepositoryMock;
        private Mock<IJudicialBlockAcl> _judicialBlockAclMock;

        [SetUp]
        public void SetUp()
        {
            _cancellationToken = default;

            _investmentAccountIdValue = Guid.NewGuid();
            _investmentAccountId = new InvestmentAccountId(
                _investmentAccountIdValue);

            _quotaRepositoryMock = new Mock<IQuotaRepository>();
            _judicialBlockAclMock = new Mock<IJudicialBlockAcl>();

            _request = new RedemptionQuotaCommandRequest(
                _investmentAccountIdValue);

            _handler = new RedemptionQuotaCommandHandler(
                _quotaRepositoryMock.Object,
                _judicialBlockAclMock.Object);
        }

        [Test]
        public void AssertExistsQuotaToRedemption()
        {
            //arrange
            var expectedExceptionMessage = "Não existe uma quota disponível para realizar o resgate";

            //act
            Task AsyncTestDelegate()
            {
                return _handler.Handle(
                    _request,
                    _cancellationToken);
            };

            //assert            
            var exception = Assert
                .ThrowsAsync<NotExistsQuotaToRedemptionException>(AsyncTestDelegate);
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [Test]
        public void AssertHasNoJudicialBlockToRedemption()
        {
            //arrange
            var getBlockedValuesResponse = new GetBlockedValuesResponse
            {
                Value = 1000
            };

            var quotas = new[]
            {
                Quota.FromPurchase(new QuotaAmount(10), _investmentAccountId),
                Quota.FromPurchase(new QuotaAmount(20), _investmentAccountId)
            };

            _quotaRepositoryMock
                .Setup(x => x.GetAllQuotasAsync(_investmentAccountId, _cancellationToken))
                .ReturnsAsync(quotas);

            _judicialBlockAclMock
                .Setup(x => x.GetBlockedValuesAsync(_investmentAccountId, _cancellationToken))
                .ReturnsAsync(getBlockedValuesResponse);

            var expectedExceptionMessage = "Este cliente possui valores bloqueados que ultrapassam a quantidade de investimentos";

            //act
            Task AsyncTestDelegate()
            {
                return _handler.Handle(
                    _request,
                    _cancellationToken);
            };

            //assert            
            var exception = Assert
                .ThrowsAsync<HasJudicialBlockToRedemptionException>(AsyncTestDelegate);
            Assert.AreEqual(expectedExceptionMessage, exception.Message);
        }

        [Test]
        public void AssertNewQuotaSaleOnRedemption()
        {
            //arrange
            var quotaAmountValue = 200;
            var quotaAmount = new QuotaAmount(
                quotaAmountValue);

            var quota = Quota.FromPurchase(
                quotaAmount,
                _investmentAccountId);

            var redeemValue = 100M;
            var redeemNegativeValue = redeemValue * -1M;

            //act
            var newQuotaSale = quota
                .Redeem(redeemValue);

            //assert
            Assert.IsNotNull(newQuotaSale);
            Assert.IsTrue(newQuotaSale.Amount.Equals(redeemNegativeValue));
            Assert.AreEqual(redeemNegativeValue, newQuotaSale.Amount.Value);
        }
    }
}
