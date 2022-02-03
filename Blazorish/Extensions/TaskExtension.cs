using System.Runtime.CompilerServices;

namespace Blazorish.Extensions;

public static class TaskExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static async Task<TResult> AsTask<T, TResult>(this Task<T> task) 
        where T : TResult 
        where TResult : class
    {
        return await task;
    }
}