using System.Reflection;

namespace Faker.Core.Models;

public record PickerModel (PropertyInfo PropertyInfo, Type ClassType);