﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaza_Tech.BLL.Services.Interfaces
{
	public interface IFileService
	{
		Task<string> SaveFileAsync(IFormFile file, string rootPath, string folderPath);
		void DeleteFile(string rootPath, string filePath);
	}
}
