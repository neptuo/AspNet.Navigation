# AspNet.Navigation
Model (class) base navigation and routing for ASP.NET (Core) MVC

## Introduction

The ASP.NET routing and link creation is missing type safety. In this library the routing is based on models, a C# define classes for each route. We can quite simply satify default route from MVC with model:

```C#

[Route("{Controller}/{Action}/{Id?}")
public class MvcRoute : RouteModel
{
    public string Controller { get; private set; }
    public string Action { get; private set; }
    public int? Id { get; private set; }
    
    public MvcRoute(string controller, string action, int? id = null)
        : base(null)
    {
        Controller = controller;
        Action = action;
        Id = id;
    }
}

```

And then, when creating link in Razor's `.cshtml`, we can use:

```Razor
@Html.ModelLink("Go Home!", new MvcRoute("Home", "Index"))
```

This is a bit better, but not that much. Let's see how we can declare route from blog post. We want url in form like `blog/{Year}/{Month}/{Day}/{Slug}`. In the standart MVC, we need to create route link like this:

```Razor
@Html.RouteLink("View post", new { Year = post.PublishedAt.Year, Month = post.PublishedAt.Month, Day = post.PublishedAt.Day, Slug = post.Slug })
```

But when using `Neptuo.AspNet.Navigation`, we can do the same with this code:

```Razor
@Html.ModelLink("View post", new BlogPostRoute(post.PublishedAt, post.Slug))
```

We also benefit from the fact, then the model can deconstruct the date itself. So route model is defined like:

```C#

[Route("blog/{Year}/{Month}/{Day}/{Slug}")]
public class BlogPostRoute : RouteModel
{
    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }
    public string Slug { get; private set; }
    
    public BlogPostRoute(DateTime publishedAt, string slug)
    {
        Year = publishedAt.Year;
        Month = publishedAt.Month;
        Day = publishedAt.Day;
        Slug = slug;
    }
}

```

## Route registration

The registration process is pretty straightforward, just add the model to the route collection using extension method:

```C#
routes.MapModel<BlogPostRoute>();
```

## Reading the parameters in the controller action method

We can easily use the route class in the parameter of action method to bind-in parameters. Note that for this use the property setters
must be public.

```C#

public class BlogController
{
    public ActionResult Post(BlogPostRoute parameters)
    {
        // TODO: Find blog post by Year, Month, Day and Slug...
    }
}


```
# License

[Apache 2.0](LICENSE)
