﻿namespace Shopify.Models.Responses
{
    public class UserRegistrationResponse
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
    }
}