# HttpController
A prototype of Http operation。(一种HTTP操作的原型)

------

山不厌高 海不厌深。

计算机技术就像浩瀚大海一样学之不完，程序员们面对技术也应该要做到持之以恒，不管是coding还是对业务的思考。

我也是一小小程序员，主要做C# C/S方向，比较喜欢本质论方面的书籍，学术不深，但喜欢捣弄。业余时间做了一个小工具，用来对web服务器进行访问操作。
这个工具包含下面内容：

> * 使用EntityFramework4.1，方便配置文件读写
> * Http内容参数化读写
> * WPF客户端作为前端(一周时间自学)
> * 一个最简易的流程控制

![image](https://raw.githubusercontent.com/CZHsoft/HttpController/master/CZHSoft_Logo_png.png)

您还可以前往以下网址查看：

### [一种HTTP操作的原型](https://github.com/CZHsoft/HttpController)

> 当然，如果有大神能把流程控制功能做到更加丰满那就跟开心了，这是我希望看见的。

------

## 主界面
![image](https://raw.githubusercontent.com/CZHsoft/HttpController/master/pic0.png)

主界面有三个按钮，包含了三种操作。

### 1. 流程配置
![image](https://raw.githubusercontent.com/CZHsoft/HttpController/master/pic1.png)

用来定义Http操作的具体流程链路，当前拿了jobcn网站作为实验。具体的参数如何填充，可以通过Http分析器捕获的具体网页操作来填入对应的参数信息。

### 2. 配置信息
![image](https://raw.githubusercontent.com/CZHsoft/HttpController/master/pic2.png)

由于进行POST操作的时候需要把对应参数信息拿到并一起发送，所以这里把参数信息通过Http分析器导出Excel文件或者直接用Excel编辑并保存。

### 3. 程序运行
![image](https://raw.githubusercontent.com/CZHsoft/HttpController/master/pic3.png)

输入对应的参数，测试具体操作结果。

作者 [@chenandczh][1] 
QQ: 1124407651   
2016 年 07月 26日    

[1]: https://github.com/chenandczh