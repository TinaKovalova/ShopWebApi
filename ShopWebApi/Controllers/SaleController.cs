using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        IService<SaleDTO> saleService;
        IService<SalePosDTO> posService;
        DbContext context;
        public SaleController()
        {
            this.context = new ShopAdoContext();
            this.saleService = new SaleService(new SaleRepository(context));
            this.posService = new SalePosService(new SalePosRepository(context));
        }
        [HttpGet]
        public ActionResult<List<SaleDTO>> Get()
        {
            return saleService.GetAll().ToList();
        }


        [HttpPost]
        public void Create([FromBody] SaleDTO sale)
        {
            var list = saleService.GetAll().ToList();
            if(list.Count==0)
            {
                sale.NumberSale = 1;
            }
            else
            {
                var last=list.OrderByDescending(i => i.NumberSale).First();
                sale.NumberSale = last.NumberSale + 1;
            }
                        
            sale.DateSale = DateTime.Now;       ///***
            saleService.CreateOrUpdate(sale);           ////**
            var last1 = saleService.GetAll().ToList().Last();           ////**
            sale.SalePos.ToList().ForEach(elem =>
            {
                elem.SaleId = last1.SaleId;
                posService.CreateOrUpdate(elem);
            });
        }
    }
}
