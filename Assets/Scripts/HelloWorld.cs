using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Book
{
    public string ISBN { get; set; } = string.Empty;
    public Book(string _ISBN)
    {
        ISBN = _ISBN;
    }
}

public class HelloWorld : MonoBehaviour
{
    private int _foo = 123;
    public int Foo => _foo;


    List<Book> bookList = new List<Book>();

    // Start is called before the first frame update
    void Start()
    {
        bookList.Add(new Book("aaa"));
        bookList.Add(new Book("bbb"));

        Debug.Log("Hello World!");
        Debug.Log(Foo);

        Book bookToFind = bookList.Find(book => book.ISBN == "aaa");

        Debug.Log(bookToFind.ISBN);

        List<int> numbers = new List<int>() { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

        // The query variable can also be implicitly typed by using var
        IEnumerable<int> filteringQuery =
            from num in numbers
            where num < 3 || num > 7
            select num;

        Debug.Log(filteringQuery);
    }

    // Update is called once per frame
    void Update()
    {

    }
}