using FluentValidation;

namespace PracticeCA.Application;

public interface IValidatorProvider
{
    IValidator<T> GetValidator<T>();
}
