using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using Fase04.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RabbitMQ.Client;
using Fase04.Infra.Messages.Helpers;
using Fase04.Consumer;
using Fase04.Infra.Message.Settings;
using RabbitMQ.Client.Events;
using FluentAssertions;

namespace Fase04.IntegrationTests
{
    public class WorkerIntegrationTests
    {
        private readonly Mock<IConnection> _mockConnection;
        private readonly Mock<RabbitMQ.Client.IModel> _mockModel;
        private readonly Mock<IMailHelper> _mockMailHelper;
        private readonly Mock<ILogger<Worker>> _mockLogger;
        private readonly IServiceProvider _serviceProvider;
        private readonly MessageSettings _messageSettings;

        public WorkerIntegrationTests()
        {
            _mockConnection = new Mock<IConnection>();
            _mockModel = new Mock<IModel>();
            _mockMailHelper = new Mock<IMailHelper>();
            _mockLogger = new Mock<ILogger<Worker>>();
            var serviceCollection = new ServiceCollection();
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _messageSettings = new MessageSettings { Queue = "test_queue" };

            _mockConnection.Setup(c => c.IsOpen).Returns(true);
            _mockModel.Setup(m => m.IsOpen).Returns(true);
        }

        [Fact]
        public async Task Worker_IniciaCorretamente_QuandoConexaoEstaAberta()
        {
            // Arrange
            var worker = new Worker(
                _mockConnection.Object,
                _mockModel.Object,
                Options.Create(_messageSettings),
                _mockMailHelper.Object,
                _mockLogger.Object,
                _serviceProvider
            );

            // Act
            var executeTask = worker.StartAsync(CancellationToken.None);
            await Task.Delay(100); 

            // Assert
            executeTask.IsCompleted.Equals(true);

            await worker.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Worker_FalhaAoIniciar_QuandoConexaoEstaFechada()
        {
            // Arrange
            _mockConnection.Setup(c => c.IsOpen).Returns(false);

            var worker = new Worker(
                _mockConnection.Object,
                _mockModel.Object,
                Options.Create(_messageSettings),
                _mockMailHelper.Object,
                _mockLogger.Object,
                _serviceProvider
            );

            // Act
            var executeTask = worker.StartAsync(CancellationToken.None);
            await Task.Delay(100); 

            // Assert
            executeTask.IsCompleted.Equals(true);

            await worker.StopAsync(CancellationToken.None);
        }

        [Fact]
        public async Task Worker_ProcessaMensagemComSucesso()
        {
            // Arrange
            var worker = new Worker(
                _mockConnection.Object,
                _mockModel.Object,
                Options.Create(_messageSettings),
                _mockMailHelper.Object,
                _mockLogger.Object,
                _serviceProvider
            );

            var content = new MessageQueueModel { Tipo = TipoMensagem.INSERIR_CONTATO, Conteudo = "{\"Nome\":\"Teste\"}" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));
            var eventArgs = new BasicDeliverEventArgs
            {
                Body = new ReadOnlyMemory<byte>(body)
            };

            _mockModel.Setup(m => m.BasicConsume(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>(), It.IsAny<IBasicConsumer>()))
                      .Callback<string, bool, string, bool, bool, IDictionary<string, object>, IBasicConsumer>((queue, autoAck, consumerTag, noLocal, exclusive, arguments, consumer) =>
                      {
                          ((EventingBasicConsumer)consumer).HandleBasicDeliver("tag", 1, false, "exchange", "routingKey", eventArgs.BasicProperties, body);
                      });

            // Act
            var executeTask = worker.StartAsync(CancellationToken.None);
            await Task.Delay(100); 

            // Assert
            executeTask.IsCompleted.Equals(true);
            executeTask.Exception.Should().BeNull();

            await worker.StopAsync(CancellationToken.None);
        }
    }
}