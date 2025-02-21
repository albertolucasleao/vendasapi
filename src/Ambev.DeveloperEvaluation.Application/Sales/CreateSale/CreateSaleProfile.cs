﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<CreateSaleProductCommand, SaleProduct>();

        CreateMap<Sale, CreateSaleResult>();
        CreateMap<SaleProduct, CreateSaleProductResult>();
    }
}
