using MediatR;
using System.Text;
using System.Diagnostics;
using Tekton.Application.Models;
using Microsoft.Extensions.Options;

namespace Tekton.Application.Behaviours
{
	public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly IOptions<Parameters> _parameters;

        public PerformanceBehaviour(IOptions<Parameters> parameters)
        {
            _parameters = parameters;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            var requestName = typeof(TRequest).Name;
            var logMessage = $"{DateTime.Now}: Long Running Request: {requestName} ({elapsedMilliseconds} milliseconds) - {Environment.NewLine}{request}{Environment.NewLine}";
            await WriteToFileAsync(logMessage);

            return response;
        }

        private async Task WriteToFileAsync(string logMessage)
        {
            try
            {
                await using (StreamWriter writer = new(_parameters.Value.PathPerformanceFile, true, Encoding.UTF8))
                {
                    await writer.WriteLineAsync(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de registro: {ex.Message}");
            }
        }

    }
}

