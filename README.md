# Meeting System for Android

此系統為國立臺北科技大學電資學院使用於正式人事升遷相關會議，包括教師升等、教職員聘任、校務會議法規制定等的數位系統。

  - 評分
  - 投票
  - 序位
  - 檔案

### Tech

Meeting System uses a number of open source projects to work properly:

* [Java] - for meeting client
* [C#] - for meeting server
* [HTML] - convert excel to html
* [jQuery] - duh

### IDE

Meeting System is currently extended with the following plugins. Instructions on how to use them in your own application are linked below.

| IDE | Reference |
| ------ | ------ |
| Android Studio | [developer.android.com][IDEAS] |
| Visual Studio | [visualstudio.microsoft.com][IDEVS] |

### Installation

For client environments...

```sh
1 Import MeetingSytem project of MeetingClient by Android Studio
2 Add google() into repositories {...} of build.gradle file
3 Click Sync Now
4 Run app on a real device
```

For server environments...

```sh
1 控制台 > 開啟或關閉 Windows 功能
2 打勾 Internet Information Services
3 將 MeetingWeb 複製到 C:\inetpub
4 開啟 IIS > 站台 > Default Web Site > 基本設定
5 改路徑 C:\inetpub\MeetingWeb
```

### Usage

The excel files of each function.

| 功能 | 選票 | 結果 |
| ------ | ------ | ------ |
| 評分 | 升等評分.xlsx | 升等評分結果.xlsx |
| 投票 | 同意票.xlsx | 同意票_選票檔.xlsx |
| 序位 | 序位投票.xlsx | 序位投票_選票檔.xlsx |

### Notice

 - 以系統管理員身份執行 server
 - 評分、投票、序位需複製新的 excel
 - 調整 excel 表格後要修改 inetpub/MeetingWeb 這個把 excel 轉成 html 的套件

License
----

MIT

**Free Software, Hell Yeah!**

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [Java]: <https://java.com>
   [C#]: <https://docs.microsoft.com/en-us/dotnet/csharp>
   [HTML]: <https://html.com>
   [jQuery]: <https://jquery.com>

   [IDEAS]: <https://developer.android.com/studio>
   [IDEVS]: <https://visualstudio.microsoft.com>
