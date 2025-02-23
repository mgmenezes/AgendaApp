using AgendaApp.Application.Commands.Contatos.CriarContato;
using AgendaApp.Domain.Entities;
using AgendaApp.Domain.Interfaces;
using AgendaApp.Application.Interfaces;
using AgendaApp.Application.MessageBus.Interfaces;
using FluentValidation;
using Moq;
using Xunit;

namespace AgendaApp.UnitTests.Commands.Contatos
{
    public class CriarContatoCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMessageBusService> _mockMessageBus;
        private readonly CriarContatoCommandHandler _handler;
        private readonly Mock<IContatoRepository> _mockContatoRepository;

        public CriarContatoCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMessageBus = new Mock<IMessageBusService>();
            _mockContatoRepository = new Mock<IContatoRepository>();

            _mockUnitOfWork.Setup(uow => uow.GetRepository<IContatoRepository>())
                          .Returns(_mockContatoRepository.Object);

            _handler = new CriarContatoCommandHandler(_mockUnitOfWork.Object, _mockMessageBus.Object);
        }

        [Fact]
        public async Task Handle_QuandoComandoValido_DeveRetornarSucesso()
        {
            // Arrange
            var command = new CriarContatoCommand
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "11999999999"
            };

            var contato = new Contato(command.Nome, command.Email, command.Telefone);

            _mockContatoRepository.Setup(r => r.AdicionarAsync(It.IsAny<Contato>()))
                                 .ReturnsAsync(contato);

            _mockMessageBus.Setup(m => m.PublishAsync(It.IsAny<string>(), It.IsAny<object>()))
                          .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.Nome, result.Nome);
            Assert.Equal(command.Email, result.Email);

            _mockContatoRepository.Verify(r => r.AdicionarAsync(It.IsAny<Contato>()), Times.Once);
            _mockMessageBus.Verify(m => m.PublishAsync(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public async Task Handle_QuandoNomeInvalido_DeveLancarExcecao()
        {
            // Arrange
            var command = new CriarContatoCommand
            {
                Nome = "",
                Email = "joao@email.com",
                Telefone = "11999999999"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}