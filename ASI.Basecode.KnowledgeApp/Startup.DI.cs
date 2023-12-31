﻿using ASI.Basecode.Data;
using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Repositories;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using ASI.Basecode.Services.Services;
using ASI.Basecode.KnowledgeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ASI.Basecode.KnowledgeApp
{
    // Other services configuration
    internal partial class StartupConfigurer
    {
        /// <summary>
        /// Configures the other services.
        /// </summary>
        private void ConfigureOtherServices()
        {
            // Framework
            this._services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            this._services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Common
            this._services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            this._services.AddScoped<IUserService, UserService>();
            this._services.AddScoped<ITrainingService, TrainingService>();
            this._services.AddScoped<ICategoryService, CategoryService>();
            this._services.AddScoped<ITopicService, TopicService>();

            // Repositories
            this._services.AddScoped<IUserRepository, UserRepository>();
            this._services.AddScoped<ITrainingRepository, TrainingRepository>();
            this._services.AddScoped<ICategoryRepository, CategoryRepository>();
            this._services.AddScoped<ITopicRepository, TopicRepository>();

            this._services.AddHttpClient();
        }
    }
}
