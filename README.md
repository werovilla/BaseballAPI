# Baseball Players API

This API provides endpoints to manage baseball player information. It allows users to retrieve, add, update, and delete player information.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [Retrieving Players](#retrieving-players)
  - [Adding a Player](#adding-a-player)
  - [Updating a Player](#updating-a-player)
  - [Deleting a Player](#deleting-a-player)
- [API Endpoints](#api-endpoints)

## Introduction

The BaseballPlayersAPI is built using ASP.NET Core and provides RESTful endpoints to manage baseball player information. It uses the `BaseballPlayersService` to interact with the data and perform CRUD operations on players.

## Getting Started

### Prerequisites

Before running the API, ensure you have the following prerequisites:

1. .NET Core SDK (Version 7.0) installed on your machine.
2. An IDE or text editor to work with the source code.

### Installation

1. Clone this repository to your local machine.
2. Open the project in your preferred IDE or text editor.
3. Build the solution to restore dependencies.

## Usage

### Retrieving Players

To retrieve a list of baseball players, make a GET request to the `/api/BaseballPlayers` endpoint. You can optionally filter the players by specifying the `bats` and `proTeam` query parameters.

#### Example:

GET /api/BaseballPlayers?bats=R&proTeam=Yankees

### Adding a Player

To add a new baseball player, make a POST request to the `/api/BaseballPlayers` endpoint with the player information provided in the request body as JSON.

#### Example:

POST /api/BaseballPlayers
Content-Type: application/json

{
"id": "200",
"firstname": "Hector",
"lastname": "Villa",
"pro_team": "Diamondbacks",
"bats": ""
}

### Updating a Player

To update an existing baseball player's information, make a PUT request to the `/api/BaseballPlayers` endpoint with the updated player information provided in the request body as JSON.

#### Example:

PUT /api/BaseballPlayers
Content-Type: application/json

{
"id": "200",
"firstname": "Hector",
"lastname": "Villa",
"pro_team": "Yankees
"bats": "R"
}

### Deleting a Player

To delete a baseball player, make a DELETE request to the `/api/BaseballPlayers/{id}` endpoint, where `{id}` is the unique identifier of the player to be deleted.

#### Example:

DELETE /api/BaseballPlayers/123

## API Endpoints

- `GET /api/BaseballPlayers`: Retrieves a list of baseball players.
  - Query Parameters:
    - `bats` (optional): Filter players by their batting hand (e.g., "L", "R").
    - `proTeam` (optional): Filter players by their professional team (e.g., "Yankees", "DiamondBacks").

- `GET /api/BaseballPlayers/{id}`: Retrieves a specific baseball player by ID.
  - Path Parameter:
    - `id`: The unique identifier of the player.

- `POST /api/BaseballPlayers`: Adds a new baseball player.
  - Request Body: PlayerInfo object in JSON format.

- `PUT /api/BaseballPlayers`: Updates an existing baseball player's information.
  - Request Body: PlayerInfo object in JSON format.

- `DELETE /api/BaseballPlayers/{id}`: Deletes a baseball player by ID.
  - Path Parameter:
    - `id`: The unique identifier of the player.

---
