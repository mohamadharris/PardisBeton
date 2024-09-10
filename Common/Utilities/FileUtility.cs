using Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class FileUtility : ITransientService
    {
        private readonly string _uploadRoot;
        private readonly string _mainRoot;

        public FileUtility(IWebHostEnvironment environment)
        {
            _mainRoot = environment.WebRootPath;
            _uploadRoot = Path.Combine(environment.WebRootPath, "uploads");
        }

        public async Task<string> UploadFileAsync(IFormFile file, string? subDirectory, string? fileName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            }
            else
            {
                fileName += Path.GetExtension(file.FileName);
            }

            string folderPath = string.IsNullOrEmpty(subDirectory) ? _uploadRoot : Path.Combine(_uploadRoot, subDirectory);
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string relativePath = string.IsNullOrEmpty(subDirectory) ? Path.Combine("uploads", fileName) : Path.Combine("uploads", subDirectory, fileName);
            return relativePath.Replace("\\", "/");
        }

        public void DeleteFile(string relativeFilePath)
        {
            string fullPath = Path.Combine(_mainRoot, relativeFilePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<List<string>> UploadMultipleFilesAsync(List<IFormFile> files, string? subPath)
        {
            List<string> result = new List<string>();
            foreach (var file in files)
            {
                result.Add(await UploadFileAsync(file, subPath, null));
            }
            return result;
        }



        public void DeleteDirectory(string path)
        {
            // Combine the root path with the provided path to get the full path
            string fullPath = Path.Combine(_uploadRoot, path);

            // Check if the provided path is a file
            if (File.Exists(fullPath))
            {
                // If it's a file, get the directory name
                fullPath = Path.GetDirectoryName(fullPath);
            }

            // Now, check if the directory exists and delete it
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, true); // true parameter ensures all contents are deleted
            }
        }
    }
}