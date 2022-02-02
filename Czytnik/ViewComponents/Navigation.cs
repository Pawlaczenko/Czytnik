﻿using Czytnik.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class Navigation : ViewComponent
{
    private readonly ICartService _cartService;

    public Navigation(ICartService cartService)
    {
        _cartService = cartService;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        int? quantity = await _cartService.GetCartQuantity();
        if(quantity == -1) quantity = null;
        return View("Default", quantity);
    }
}
