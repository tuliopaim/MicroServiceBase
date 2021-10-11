using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CQRS.Core.API;
using CQRS.Core.Application;

namespace CQRS.BenchmarkConsole
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BenchmarkHandleMediatorResult
    {
        private readonly List<ClasseDeResultado> _listaDeResultados = new();

        private readonly ClasseDeResultado _resultado = new()
        {
            Id = Guid.NewGuid(),
            Nome = "Pessoa"
        };

        public BenchmarkHandleMediatorResult()
        {
            for (var i = 0; i < 500; i++)
            {
                _listaDeResultados.Add(new ClasseDeResultado
                {
                    Id = Guid.NewGuid(),
                    Nome = $"Pessoa {i}"
                });
            }
        }

        [Benchmark(Baseline = true)]
        public void Handle500MediatorResultsComReflecion()
        {
            foreach (var resultado in _listaDeResultados)
            {
                _ = resultado.HandleComReflection();
            }
        }

        [Benchmark]
        public void Handle500MediatorResultsSemReflecion()
        {
            foreach (var resultado in _listaDeResultados)
            {
                _ = resultado.HandleMediatorResult();
            }
        }

        [Benchmark]
        public void Handle1MediatorResultComReflecion()
        {
            _ = _resultado.HandleComReflection();
        }

        [Benchmark]
        public void Handle1MediatorResultSemReflecion()
        {

            _ = _resultado.HandleMediatorResult();
        }

        protected class ClasseDeResultado : MediatorResult
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }
        }
    }
}