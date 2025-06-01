using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Tests.Common;

public class ResultTests
{
    [Fact]
    public void Constructor_WhenInitializedWithStringValue_ShouldSetValue()
    {
        // Arrange
        var expectedString = "test string";

        // Act
        var result = new Result<string>(expectedString);

        // Assert
        Assert.Equal(expectedString, result.Value);
    }

    [Fact]
    public void Constructor_WhenInitializedWithIntValue_ShouldSetValue()
    {
        // Arrange
        var expectedInt = 123;

        // Act
        var result = new Result<int>(expectedInt);

        // Assert
        Assert.Equal(expectedInt, result.Value);
    }

    [Fact]
    public void Constructor_WhenInitializedWithBoolValue_ShouldSetValue()
    {
        // Arrange
        var expectedBool = true;

        // Act
        var result = new Result<bool>(expectedBool);

        // Assert
        Assert.Equal(expectedBool, result.Value);
    }

    [Fact]
    public void Constructor_WhenInitializedWithObjectValue_ShouldSetValue()
    {
        // Arrange
        var expectedObject = new TestObject();

        // Act
        var result = new Result<TestObject>(expectedObject);

        // Assert
        Assert.Equal(expectedObject, result.Value);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void Constructor_WhenInitializedWithValue_ShouldSetStatusToOk(object value)
    {
        // Arrange

        // Act
        var result = new Result<object>(value);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void Success_WhenCalledWithValue_ShouldSetStatusToOk(object value)
    {
        // Arrange

        // Act
        var result = Result<object>.Success(value);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(value, result.Value);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void SuccessGeneric_WhenCalledWithValue_ShouldSetStatusToOk(object value)
    {
        // Arrange

        // Act
        var result = Result.Success(value);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal(value, result.Value);
        Assert.True(result.IsSuccess);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void Created_WhenCalledWithValueAndLocation_ShouldSetStatusToCreated(object value)
    {
        // Arrange
        var location = "test string";

        // Act
        var result = Result<object>.Created(value, location);

        // Assert
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.Equal(location, result.Location);
        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData("test string")]
    [InlineData(123)]
    [InlineData(true)]
    public void CreatedGeneric_WhenCalledWithValueAndLocation_ShouldSetStatusToCreated(object value)
    {
        // Arrange
        var location = "test string";

        // Act
        var result = Result.Created(value, location);

        // Assert
        Assert.Equal(ResultStatus.Created, result.Status);
        Assert.Equal(location, result.Location);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void NoContent_WhenCalled_ShouldSetStatusToNoContent()
    {
        // Arrange

        // Act
        var result = Result<object>.NoContent();

        // Assert
        Assert.Equal(ResultStatus.NoContent, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void NoContentGeneric_WhenCalled_ShouldSetStatusToNoContent()
    {
        // Arrange

        // Act
        var result = Result.NoContent();

        // Assert
        Assert.Equal(ResultStatus.NoContent, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Error_WhenCalled_ShouldSetStatusToError()
    {
        // Arrange

        // Act
        var result = Result<object>.Error();

        // Assert
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void ErrorGeneric_WhenCalled_ShouldSetStatusToError()
    {
        // Arrange

        // Act
        var result = Result.Error();

        // Assert
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Unauthorized_WhenCalled_ShouldSetStatusToUnauthorized()
    {
        // Arrange

        // Act
        var result = Result<object>.Unauthorized();

        // Assert
        Assert.Equal(ResultStatus.Unauthorized, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void UnauthorizedGeneric_WhenCalled_ShouldSetStatusToUnauthorized()
    {
        // Arrange

        // Act
        var result = Result.Unauthorized();

        // Assert
        Assert.Equal(ResultStatus.Unauthorized, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Forbidden_WhenCalled_ShouldSetStatusToForbidden()
    {
        // Arrange

        // Act
        var result = Result<object>.Forbidden();

        // Assert
        Assert.Equal(ResultStatus.Forbidden, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void ForbiddenGeneric_WhenCalled_ShouldSetStatusToForbidden()
    {
        // Arrange

        // Act
        var result = Result.Forbidden();

        // Assert
        Assert.Equal(ResultStatus.Forbidden, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void NotFound_WhenCalled_ShouldSetStatusToNotFound()
    {
        // Arrange

        // Act
        var result = Result<object>.NotFound();

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void NotFoundGeneric_WhenCalled_ShouldSetStatusToNotFound()
    {
        // Arrange

        // Act
        var result = Result.NotFound();

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void GetValue_WhenCalled_ShouldReturnValue()
    {
        // Arrange
        var expectedValue = "test value";
        var result = new Result<string>(expectedValue);

        // Act
        var value = result.GetValue();

        // Assert
        Assert.Equal(expectedValue, value);
    }

    [Fact]
    public void GetValue_WhenCalledOnResultWithNullValue_ShouldReturnNull()
    {
        // Arrange
        var result = new Result<string>(null);

        // Act
        var value = result.GetValue();

        // Assert
        Assert.Null(value);
    }

    [Fact]
    public void ImplicitConversion_WhenStringValueProvided_ShouldSetStatusToOkAndSetValue()
    {
        // Arrange
        var expectedString = "test string";

        // Act
        Result<string> result = expectedString;

        // Assert
        Assert.Equal(expectedString, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public void ImplicitConversion_WhenIntValueProvided_ShouldSetStatusToOkAndSetValue()
    {
        // Arrange
        var expectedInt = 123;

        // Act
        Result<int> result = expectedInt;

        // Assert
        Assert.Equal(expectedInt, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public void ImplicitConversion_WhenBoolValueProvided_ShouldSetStatusToOkAndSetValue()
    {
        // Arrange
        var expectedBool = true;

        // Act
        Result<bool> result = expectedBool;

        // Assert
        Assert.Equal(expectedBool, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    [Fact]
    public void ImplicitConversion_WhenObjectValueProvided_ShouldSetStatusToOkAndSetValue()
    {
        // Arrange
        var expectedObject = new TestObject();

        // Act
        Result<TestObject> result = expectedObject;

        // Assert
        Assert.Equal(expectedObject, result.Value);
        Assert.Equal(ResultStatus.Ok, result.Status);
    }

    private record TestObject;
}
