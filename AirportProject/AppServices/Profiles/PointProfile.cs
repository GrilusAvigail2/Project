#pragma warning disable IDE0005 // Using directive is unnecessary.
using AutoMapper;
using Common;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Profiles
{
    class PointProfile:Profile
    {
        public PointProfile()
        {
            CreateMap<Points, CPoint>();
            CreateMap<CPoint,Vertex>();
            CreateMap<CPoint, Points>();
            CreateMap<Vertex, CPoint>();
        }
    }
}
