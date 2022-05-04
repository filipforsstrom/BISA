﻿using BISA.Shared.Entities;

namespace BISA.Server.Services.LibrisService
{
    public interface ILibrisService
    {
        Task<List<LibrisItemDTO>> GetItems();
        Task<List<LibrisItemDTO>> GetItem(string ISBN);
    }
}
