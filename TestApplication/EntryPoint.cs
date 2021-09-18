using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using TestApplication.Extentions;
using TestApplication.Services;
using TestApplication.Utility;

namespace TestApplication
{
    public class EntryPoint
    {
        private readonly IDataRepository _repository;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        public EntryPoint(IDataRepository repository, IFileService fileService, IConfiguration configuration) =>
                         (_repository, _fileService, _configuration) = (repository, fileService, configuration);

        public async Task Run()
        {
            bool endApp = false;

            while (!endApp)
            {
                try
                {
                    Console.Write("Введите Id пользователя:");

                    if (int.TryParse(Console.ReadLine(), out int userId) && userId > 0)
                    {
                        var userTask = _repository.GetUserByIdAsync(userId);

                        var spinner = new ConsoleSpinner(500);

                        while (userTask.Status != TaskStatus.RanToCompletion && userTask.Status != TaskStatus.Faulted)
                        {
                            await spinner.Turn("Получение информации о пользователе", 0);
                        }

                        if (userTask.Exception is null)
                        {
                            await Task.WhenAll(userTask);

                            var filePathText = userTask.Result.GetFilePathText();

                            await _fileService.WriteTextInFile(filePathText);

                            Console.Clear();

                            Console.WriteLine(filePathText.fileText);
                            var filePath = Path.Combine(_configuration["FilePath"], filePathText.filePath);

                            Console.WriteLine($"-------------Файл с текущим текстом успешно сохранен в {filePath}-------------\n"); ;
                            ShowCommonInformation(ref endApp);
                        }
                        else
                        {
                            Console.WriteLine(userTask.Exception.InnerException.Message);
                            userTask.Dispose();
                            ShowCommonInformation(ref endApp);
                        }
                    }
                    else Console.WriteLine("Значение должно быть числовым и больше нуля");
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($"Во время работы программы произошла ошибка: {ex.Message}");
                    ShowCommonInformation(ref endApp);
                }
            }
        }

        private static void ShowCommonInformation(ref bool endApp)
        {
            Console.WriteLine("Для продолжения работы с программой нажмите любую клавишу\nДля выхода из программы нажмите Q");
            if (Console.ReadKey().Key == ConsoleKey.Q) endApp = true;
            else Console.Clear();
        }
    }
}