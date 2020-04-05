using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Project1
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSerilog();
                });
        }
    }
}


/*
TODO place orders to store locations for customers
// TODO add a new customer
// TODO search customers by name
TODO display details of an order
TODO display all order history of a store location
TODO display all order history of a customer
// TODO client-side validation [new]
// TODO server-side validation [new]
// TODO exception handling
TODO CSRF prevention
// TODO persistent data; no prices, customers, order history, etc. hardcoded in C#
// TODO logging [new]
TODO (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
TODO (optional: get a suggested order for a customer based on his order history)
TODO (optional: display some statistics based on order history)
TODO (optional: asynchronous network & file I/O)
TODO (optional: deserialize data from disk)
TODO (optional: serialize data to disk)
// TODO use EF Core (either database-first approach or code-first approach)
// TODO use an Azure SQL DB in third normal form
// TODO don't use public fields
// TODO define and use at least one interface
// TODO core / domain / business logic
// TODO library contains all business logic
// TODO library contains domain classes (customer, order, store, product, etc.)
TODO documentation with <summary> XML comments on all public types and members (optional: <params> and <return>)
// TODO customer has first name, last name, etc.
// TODO customer (optional: has a default store location to order from)
// TODO order has a store location
// TODO order has a customer
// TODO order has an order time (when the order was placed)
// TODO order can contain multiple kinds of product in the same order
// TODO order rejects orders with unreasonably high product quantities
TODO (optional: some additional business rules, like special deals)
// TODO location has an inventory
// TODO location inventory decreases when orders are accepted
// TODO location rejects orders that cannot be fulfilled with remaining inventory
TODO (optional: for at least one product, more than one inventory item decrements when ordering that product)
TODO user interface
// TODO ASP.NET Core MVC web application [new]
// TODO separate request processing and presentation concerns with MVC pattern [new]
// TODO strongly-typed views [new]
// TODO minimize logic in views [new]
// TODO use dependency injection [new]
TODO customize the default styling to some extent [new]
// TODO keep CodeNamesLikeThis out of the visible UI [new]
// TODO class library
// TODO contains EF Core DbContext and entity classes
// TODO contains data access logic but no business logic
// TODO use repository pattern for separation of concerns
TODO at least 10 test methods
TODO focus on unit testing business logic
TODO data access tests (if present) should not impact the app's actual database [new]
*/

/*
 * in package manager console..
 * 1. add-migration initialmigration
 * 2. update-database migrationname
 * */
