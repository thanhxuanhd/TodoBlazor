using System;
using System.Collections.Generic;

namespace Todo.App.Models;

public class PaginatedTodoListViewModel
{
    public PaginatedTodoListViewModel()
    {
    }

    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public int TotalRecords { get; set; }

    public List<TodoViewModel> Items { get; set; }

    public PaginatedTodoListViewModel(List<TodoViewModel> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalRecords = count;

        Items = new List<TodoViewModel>();
        Items.AddRange(items);
    }

    public bool HasPreviousPage
    {
        get
        {
            return (PageIndex > 1);
        }
        set { }
    }

    public bool HasNextPage
    {
        get
        {
            return (PageIndex < TotalPages);
        }
        set { }
    }
}