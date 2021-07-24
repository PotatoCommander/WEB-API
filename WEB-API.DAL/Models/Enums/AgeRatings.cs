using System;
using System.Diagnostics.CodeAnalysis;

namespace WEB_API.DAL.Models.Enums
{
    public enum AgeRatings: byte
    {
        None,
        PEGI3,
        PEGI7,
        PEGI12,
        PEGI16,
        PEGI18
    }
}