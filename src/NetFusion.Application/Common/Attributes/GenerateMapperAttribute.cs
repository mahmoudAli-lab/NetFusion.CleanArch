[AttributeUsage(AttributeTargets.Class)]
public class GenerateMapperAttribute : Attribute
{
    public Type TargetType { get; }
    
    public GenerateMapperAttribute(Type targetType)
    {
        TargetType = targetType;
    }
}

// Usage Example
[GenerateMapper(typeof(UserDto))]
public partial class User : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    // Source generator will create ToDto() method
}
