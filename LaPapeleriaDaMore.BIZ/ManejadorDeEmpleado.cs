﻿using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.BIZ
{
    public class ManejadorDeEmpleado : IManejadorDeEmpleado
    {
        IRepositorio<Empleado> repositorio;
        public ManejadorDeEmpleado(IRepositorio<Empleado> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Empleado> Listar => repositorio.Leer;

        public bool Agregar(Empleado entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Empleado BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Empleado entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
