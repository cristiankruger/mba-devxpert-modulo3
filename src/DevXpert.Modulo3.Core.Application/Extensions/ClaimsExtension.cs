﻿using System.Security.Claims;

namespace DevXpert.Modulo3.Core.Application.Extensions;

public static class ClaimExtensions
{
    public static bool PossuiRole(this IEnumerable<Claim> claims, string role)
        => claims?.Any(c => c.Type == ClaimTypes.Role && c.Value == role) == true;
}
