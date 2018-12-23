using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS_420_WebApplication.Data;
using Microsoft.EntityFrameworkCore;
using CIS_420_WebApplication.Models;
using CIS_420_WebApplication.Interfaces;
namespace CIS_420_WebApplication.Controllers
{
    public class CRUDHelper<T,T1> where T : Controller where T1:class, IIdentifier<T1>
    {
        public static async Task<IActionResult> Creator(T type,T1 model, ApplicationDbContext context, bool modelValid, string action) 
        {
            if (modelValid)
            {
                context.Add(model);
                await context.SaveChangesAsync();
                return type.RedirectToAction(action);
            }
            return type.View(model);
        }

        public static async Task<IActionResult> FindDetails(T type, ApplicationDbContext context, int? id)
        {
            if (id == null)
            {
                return type.NotFound();
            }

            var model = await context.FindAsync<T1>(id);

            if (model == null)
            {
                return type.NotFound();
            }

            return type.View(model);
        }

        public static async Task<IActionResult> EditView(T type,ApplicationDbContext context, int? id)
        {
            if(id==null)
                return type.NotFound();
            var model = await context.FindAsync<T1>(id);
            if (model == null)
            {
                return type.NotFound();
            }
            return type.View(model);
        }
        public static async Task<IActionResult> Editor(T type,T1 tModel, ApplicationDbContext context, bool modelValid,string indexName,int? id)
        {
            if (id !=tModel.UId)
                return type.NotFound();
          
            if (modelValid)
            {
                try
                {
                    context.Update(tModel);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (context.FindAsync<T1>()==null)
                    {
                        return type.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return type.RedirectToAction(indexName);
            }
            return type.View(tModel);
        }
        public static async Task<IActionResult> DeleteView(T type, ApplicationDbContext context,int? id)
        {
            if (!id.HasValue)
            {
                return type.NotFound();
            }
            var model= await context.FindAsync<T1>(id);
            if (model==null)
            {
                return type.NotFound();
            }
            return type.View(model);
        }
        public static async Task<IActionResult> Deletor(T type, ApplicationDbContext context,string action, int? id)
        {
            if (id.HasValue)
            {
                var model = await context.FindAsync<T1>(id.Value);
                context.Remove(model);
                await context.SaveChangesAsync();
            }

            return type.RedirectToAction(action);
        }
    }
}
