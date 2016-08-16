    *** Cast int to enum ***

(1) From a string:

    YourEnum foo = (YourEnum)Enum.Parse(typeof(YourEnum), yourString);

(2) From an int:

    YourEnum foo = (YourEnum)yourInt;

(3) From number you can also

    YourEnum foo = (YourEnum)Enum.ToObject(typeof(YourEnum) , yourInt);


(4) Alternatively, use an extension method instead of a one-liner:

    public static T ToEnum<T>(this string enumString)
    {
        return (T) Enum.Parse(typeof(T), enumString);
    }

    Usage:

        Color colorEnum = "Red".ToEnum<Color>();

    OR

        string color = "Red";
    var colorEnum = color.ToEnum<Color>();
