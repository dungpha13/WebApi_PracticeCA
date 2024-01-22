using AutoMapper;

namespace PracticeCA.Application;

interface IMapFrom<T>
{
    void Mapping(Profile profile);
}
