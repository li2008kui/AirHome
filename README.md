# AirHome 智能控制系统通信协议的实现 #
## 项目概述 ##
&emsp;&emsp;本项目是一个采用C#语言实现的智能家居控制系统通信协议。其适用于智能控制系统中智能设备模块（如WIFI、ZigBee、蓝牙和红外等）与远程服务器或本地智能设备（如手机、平板电脑、PC等）之间进行数据透明传输，协议定义了消息的功能和通信报文格式。
## 报文格式 ##
&emsp;&emsp;为了生成和解析消息具有一致性，并且方便后续扩展新的功能，所有消息数据均采用十六进制形式表示，所有功能指令均进行参数化处理。
### 消息格式定义 ###
&emsp;&emsp;消息报文格式的定义由起始符、消息头、消息体和结束符组成，其具体长度随消息体长度的改变而变化。
<table style="border-collapse: collapse; text-align: center;">
<tr><th></th><th>起始符</th><th>消息头</th><th>消息体</th><th>结束符</th></tr>
<tr><th>表示</th><td>STX</td><td>HEAD</td><td>BODY</td><td>ETX</td></tr>
<tr><th>长度</th><td>1Byte</td><td>12Byte</td><td>可变</td><td>1Byte</td></tr>
<tr><th>取值</th><td>0X02</td><td></td><td></td><td>0X03</td></tr>
</table>
### 转义字符定义 ###
&emsp;&emsp;当数据帧起始符和结束符之间（即消息头和消息体中）出现STX，ETX或ESC时，需要进行转义。<br/>
* STX转义为ESC和0XE7，即02->1BE7<br/>
* ETX转义为ESC和0XE8，即03->1BE8<br/>
* ESC转义为ESC和0X00，即1B->1B00<br/>
### 消息头定义 ###
&emsp;&emsp;消息头的长度总共有12个字节，依次定义了1个字节的消息类型、2个字节的消息体长度、4个字节的消息序号、3个字节的预留字段和2个字节的消息体CRC校验。
<table style="border-collapse: collapse; text-align: center;">
<tr><th></th><th>消息类型</th><th>消息体长度</th><th>消息序号</th><th>预留</th><th>CRC校验</th></tr>
<tr><th>长度</th><td>1Byte</td><td>2Byte</td><td>4Byte</td><td>3Byte</td><td>2Byte</td></tr>
<tr><th>取值</th><td>00~FF</td><td>0000~FFFF</td><td>00000000~FFFFFFFF</td><td>000000</td><td>0000~FFFF</td></tr>
</table>
**消息类型**：表示消息在设备与服务器或APP之间关系。<br/>
**消息体长度**：可变消息体所占的字节数。<br/>
**消息序号**：取值范围为0~232-1，达到最大值后自动返回从0开始。<br/>
**CRC校验**：对消息体的CRC校验，采用CRC16算法。<br/>
### 消息体定义 ###
&emsp;&emsp;消息体定义了消息ID、设备ID和参数列表，其中参数列表由多个参数依次排列，每个参数由参数类型、参数值长度和参数值组成。消息体的长度由参数值长度及参数个数不同而不固定。
<table style="border-collapse: collapse; text-align: center;">
<tr><th rowspan="2"></th><th rowspan="2">消息ID</th><th rowspan="2">设备ID</th><th colspan="4">参数列表</th></tr>
<tr><th>参数1类型</th><th>参数1值长度</th><th>参数1值</th><th>参数2...</th></tr>
<tr><th>长度</th><td>2Byte</td><td>8Byte</td><td>2Byte</td><td>1Byte</td><td>可变</td><td></td></tr>
<tr><th>取值</th><td></td><td></td><td></td><td></td><td></td><td></td></tr>
</table>
**消息ID**：报文消息的代码。<br/>
**设备ID**：用于标识设备唯一性的UID，分配方式待定，如果全为0表示广播。兼容其他厂商时，若超过8个字节，取其低（右边）8个字节；若小于8个字节，左边使用0填充。<br/>
**参数类型**：参数的类型代码。<br/>
**参数值长度**：表示某个参数值的字节数，该值不应超过它的类型规定的最大取值。<br/>
**参数值**：长度可变的字符串十六进制表示。<br/>