using libreriaa_JADM.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;

namespace libreriaa_JADM.Data
{
    public class AppDbInitialer
    {
        //Metodo que agrega dato a nuestra BD
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Titulo = "1st Book Title",
                        Descripcion = "1st Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genero = "Biography",
                        CoverUrl = "https...",
                        DateAdded = DateTime.Now,


                    },
                    new Book()
                    {
                        Titulo = "2st Book Title",
                        Descripcion = "2st Book Description",
                        IsRead = true,
                       
                        Genero = "Biography",
                        CoverUrl = "https...",
                        DateAdded = DateTime.Now,


                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
