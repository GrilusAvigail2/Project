#pragma warning disable IDE0005 // Using directive is unnecessary.
using Common;
using AutoMapper;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Profiles
{
    public class EdgeProfile:Profile
    {
        public EdgeProfile()
        {
            CreateMap<Edges, CEdge>();
        }
    }
}
