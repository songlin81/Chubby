冒泡排序算法

    int temp;
    int[] arrSort = new int[] { 10, 8, 3, 5, 6, 7, 9 };
    for (int i = 0; i < arrSort.Length; i++)
    {
        for (int j = i + 1; j < arrSort.Length; j++)
        {
            if (arrSort[j] < arrSort[i])
            {
                temp = arrSort[j];
                arrSort[j] = arrSort[i];
                arrSort[i] = temp;
            }
        }
    }