# TowerDefenceLevelEditor
The tool is a grid-based level editor for tower defense games that allows level designers to create layouts without a test or play mode. Using a dynamic canvas, various tile types can be placed, enemy routes can be defined with multiple paths and intersections, and custom content can optionally be added. The tool includes basic functionality such as tile selection, canvas resizing, and path editing, and offers save/load capabilities that save levels in JSON format.

The final output is a game-ready data file with canvas size, tile map, path nodes, and any asset mappings. The test build focuses on drawing the grid, changing tiles, JSON save/load, and basic canvas resizing. The full version expands on this with more extensive editing tools, pre-export validation, and UI improvements.

## Beta todo list
Feedback from teachers:
- ~~Custom tile do not work when loading it in when you don't have the custom tile.~~
- Constrains per tile (end and start)
- Enemy path drawing/editing
- Ctrl+Z, Ctrl+Y via command pattern
- UI/UX

## Alpha UML class diagram

```mermaid
classDiagram

    %% ===============================
    %% Main data flow centered on TilemapData
    %% ===============================

    class FileEditor {
        +TilemapData tilemapData
        +TileMap tileMap
        +GetData() TilemapData
        +SetData(TilemapData data)
    }

    class TilemapData {
        +string name
        +int rows
        +int cols
        +int[] tileId
    }

    class TileMap {
        -List~Tile~ _tiles
        -Vector2Int size
        +GetTiles() List~Tile~
        +CreateNewMap(TilemapData)
        -InitTileMap(TilemapData?)
        -CreateTile(pos)
    }

    class Tile {
        -int _id
        +SetTileId(int)
        +GetId() int
        -DoDrag()
    }

    class ToolData {
        +static Instance : ToolData
        +int SelectedTileId
        +SetSelectedTileId(int)
    }

    class InputParser {
        <<component>>
        +ReadControls()
        +InterpretInput()
        +ModifySelectedTile()
    }

    %% ===============================
    %% Relationships
    %% ===============================

    FileEditor --> TilemapData : load/save JSON data
    FileEditor --> TileMap : passes data to map

    TileMap --> TilemapData : uses size + tileIds
    TileMap --> Tile : creates and manages tiles

    Tile --> ToolData : reads SelectedTileId
    ToolData --> Tile : tile reacts to selected id changes

    InputParser --> ToolData : modifies selected tile id

```
