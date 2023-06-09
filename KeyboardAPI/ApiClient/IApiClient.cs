﻿using KeyboardAPI.DTOs;

namespace KeyboardAPI.ApiClient;

public interface IApiClient
{
    public Task<UserDTO> GetUserById(int id);
}
