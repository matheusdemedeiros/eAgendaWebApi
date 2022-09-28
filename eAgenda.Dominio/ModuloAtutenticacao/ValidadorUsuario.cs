﻿using FluentValidation;


namespace eAgenda.Dominio.ModuloAtutenticacao
{
    public class ValidadorUsuario : AbstractValidator<Usuario>
    {

        public ValidadorUsuario()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo nome é obrigatório")
                .NotEmpty().WithMessage("O campo nome é obrigatório");
        }
    }
}