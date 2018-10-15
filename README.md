# Leanware Project

'Leanware' is a revolutionary work tool for Lean Software Development. It is a simple cards board (also known as kanban) that visualizes development scope and progress. The goal of the project is to implement API for managing the board.

This assigment comes with much less ready-to-use code than before: you have to implement controllers on your own, and also add fields to models (in `Models` folder). You don't need to modify DI in `Startup` for this assignment: `LeanwareContext` is already registered for you in `Startup.Student.cs`. So most of your job in this assignment is putting together all the code you saw in previous assignment from week 5 and implementing similar solution in this project. Later on you will use this project for other assignments and develop it step by step to a bigger solution.

*NOTE*: execute `dotnet` commands from the project folder `Leanware.Web`, not from the root one.

## Domain

Domain part of this assignment is simple. There are only two entities: stories and features. Every entity has its integer ID. Every story must belong to some feature. Every feature and story has a title. Story may have description. Also stories and features may have tags (list of strings). Goal of this assignment is to implement CRUD (create, read, update, delete) API for stories and features.

## API

Implementing API from scratch is the core task of this assignment.

General convention for this API (and of REST API development in general) is:

- use POST requests to create entities;
- use PATCH requests to update entities;
- use DELETE requests to delete entities;
- use GET requests to read entities. Below are API endpoints you have to implement.

### Create feature

POST /api/features
```
    {
        "title": "some title",
        "tags": "tag1, tag2, tag3"
    }
```

Response
HTTP 200 OK or HTTP 201 Created
```
    {
        "id": 1
        "title": "some title",
        "tags": "tag1, tag2, tag3"
    }
```

### Create story

POST /api/stories
```
    {
        "title": "some title",
        "tags": "tag1, tag2, tag3",
        "featureId": 1
    }
```

Response
HTTP 200 OK or HTTP 201 Created
```
    {
        "id": 1
        "title": "some title",
        "tags": "tag1, tag2, tag3",
        "featureId": 1
    }
```

### Update feature

PATCH /api/features/1
```
    {
        "title": "new title"
    }
```

Response
HTTP 200 OK

### Update story

PATCH /api/stories/1
```
    {
        "title": "new title"
    }
```

Response
HTTP 200 OK

### Get feature

GET /api/features/1

Response
HTTP 200 OK
```
    {
        "id": 1
        "title": "new title",
        "tags": "tag1, tag2, tag3"
    }
```

### Get story

GET /api/stories/1

Response
HTTP 200 OK
```
    {
        "id": 1
        "title": "new title",
        "tags": "tag1, tag2, tag3",
        "featureId": 1
    }
```

### Delete feature

DELETE /api/features/1

Response
HTTP 200 OK

### Delete story

DELETE /api/stories/1

Response
HTTP 200 OK

#### Note

Once again: check previous assignment (w5), see how it was implemented, and try to implement this one in a similar way. There is not much domain logic (it's really primitive), so you can pay more attention to overall app infrastructure.

## Working with DB

As in previous assignment, you work with SQLite DB. But this time you have to define entities on your own - there are classes `Feature.cs` and `Story.cs` in `Models` folder - you have to work with them and modify them as you need.

To update DB schema, run provided `update-db.ps1` script (Windows) or `update-db.sh` (Linux/Mac), and EF migration will be generated for you. This migration will be automatically applied to DB when you run application.

## Testing your API

As before, Swagger UI is provided with the starter project, so you can run application and see it at `http://localhost:5000/swagger` (NOTE: 5000 port is default one, it may differ on your machine - check console output as your start app).
