﻿using System;
using FlixOne.InventoryManagement.Command;
using FlixOne.InventoryManagement.Repository;
using FlixOne.InventoryManagement.UserInterface;
using Microsoft.Extensions.DependencyInjection;

namespace FlixOne.InventoryManagementClient
{
    public class InventoryManagementClientProgram
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetService<ICatalogService>();
            service.Run();

            Console.WriteLine("CatalogService has completed.");
            Console.ReadLine();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Добавляем сервисы приложения
            services.AddTransient<IUserInterface, ConsoleUserInterface>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<IInventoryCommandFactory, InventoryCommandFactory>();

            var context = new InventoryContext();
            services.AddSingleton<IInventoryReadContext, InventoryContext>(p => context);
            services.AddSingleton<IInventoryWriteContext, InventoryContext>(p => context);
            services.AddSingleton<IInventoryContext, InventoryContext>(p => context);

        }

    }
}

