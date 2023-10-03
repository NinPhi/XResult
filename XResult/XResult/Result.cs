﻿namespace XResult;

public abstract class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsError => IsSuccess != true;

    public List<Error> Errors { get; protected set; } = new();

    protected Result() { }
}

public class Result<TValue> : Result
{
    public TValue? Value { get; private set; }
    public bool HasValue { get; private set; }

    internal Result()
    {
        IsSuccess = true;
        Value = default;
        HasValue = false;
    }

    internal Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
        HasValue = false;
    }

    internal Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        HasValue = false;
        Errors.Add(error);
    }

    internal Result(List<Error> errors)
    {
        IsSuccess = false;
        Value = default;
        HasValue = false;
        Errors = errors;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);
    public static implicit operator Result<TValue>(List<Error> errors) => new(errors);
}

public class Error
{
    public string Code { get; private set; }
    public string Message { get; private set; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}