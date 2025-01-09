# The Zene Structs Library

The Zene Structs Library is a collection of c# structs that represent/manage geographical objects and values. It is the base for the rest of The Zene Library.

To set the library to use double precision floats, set **UseDouble** to true. This is done in the .csproj file, e.g.
```
<ProjectReference Include="..\deps\Structs\src\Structs.csproj" AdditionalProperties="UseDouble=true" />
```

## Credits

Rgb and Hsl converter from [This site](http://csharphelper.com/howtos/howto_rgb_to_hls.html)<br>

Matrix functions from [OpenTK](https://github.com/opentk/opentk)<br>