# Ink Canvas Better 使用说明

**注意：本文档内容可能已经过时**

## 目录
[一、介绍](#intro)  
[二、用法](#usage)  
[三、技巧](#skill)  
[四、感谢](#thank) 
## Ink Canvas Better 简介 <span id='intro'></span>
Ink Canvas Better 是一款轻量级画板软件，针对教学环境进行开发，提供更好的教学体验  

### Ink Canvas Better 模式

* **幻灯片模式**
    *  在幻灯片模式下支持画笔、橡皮擦、图形工具、快捷翻页按键、换页自动换笔迹等功能
    *  自动保存幻灯片下的墨迹，下次打开时墨迹将自动恢复，并且随时可修改。
    *  如果有隐藏的幻灯片页面，将询问是否取消隐藏
<!-- 还没做完
* **画板模式（黑/白板模式）**
    *  在画板模式下有着一整个类似希沃白板一样的画板
    *  支持添加新页面和页面切换
    *  支持多指书写：黑板模式界面左下角人像图标为切换按钮
	*  默认是黑板，可以在设置中调成白板
-->
* **屏幕画笔模式**
    *  在屏幕画笔模式下可以显示原屏幕内容的同时将鼠标调为画笔书写授课笔迹

### Ink Canvas Better 特性
* 一键清屏，更方便
* 自动查杀希沃部分软件
* 图形绘制（支持长按一直选中）
  * 直线、虚线、带箭头直线
  * 多条平行线，带焦点和不带焦点的椭圆、双曲线、抛物线。
  * 正圆、虚线圆，圆柱、圆锥、长方体
  * 坐标系（平面直角坐标系，空间直角坐标系）
* 放大镜功能，把细小图形展示出来
<!-- 还没做完
* 手掌手指并拢，直接放在屏幕上，则是大橡皮（按区域擦除）
* 单条或多条墨迹选中后缩放、旋转、移动、克隆
* 全屏幕笔迹双指手势缩放（旋转和拖动也是双指手势）
* 墨迹转图形，目前可实现智能识别圆、三角形、四边形
  自动转换为规范图形。可自动识别同心圆，相切圆，可自动识别球的截面圆
-->
### Ink Canvas Better 功能  
* **保存墨迹**：默认保存至 `文档\Ink Canvas Strokes`
* **截图**：任意模式模式下（包括鼠标）下点击相机图标截图并自动保存至 `图片\Ink Canvas Screenshots`，可在设置中开启“截图时自动保存墨迹”
* **幻灯片自动保存墨迹**：默认保存至 `文档\Ink Canvas Strokes`
* **墨迹回放**：从头自动书写一遍屏中墨迹


## 用法 <span id='usage'></span>
### 画笔设置
* **颜色**：在“画笔模式”下直接点击主界面的颜色球即可
* **粗细**：点击“铅笔”打开面板，在面板中调节，默认3
* **笔锋**：点击“铅笔”打开面板，在面板中调节，根据“速度”绘制笔锋，让笔迹更美观
### 橡皮擦使用
* **笔迹擦除**：点击“橡皮擦”打开面板，在面板中调节
* **范围擦除**：点击“橡皮擦”打开面板，在面板中调节
<!-- 还没做完
* **手掌擦除**：在画笔模式下按压手掌即可起到橡皮擦擦除功能（部分屏幕不支持）
-->
### 绘制预设图形
* **正圆**：确定圆心时可选用预设在“原点”位置点击，再拖动鼠标完成正圆绘制
* **抛物线**：可以在“原点”位置点击，再拖动鼠标
* **双曲线**：在“原点”位置点击拖动鼠标，先确定渐近线，然后确定实轴和虚轴
* **椭圆**：在“原点”位置点击拖动鼠标，然后确定长轴和短轴（椭圆和双曲线都可以选择有无显示焦点的预设）
* **长方体**：先确定正视图，然后再次点击，确定侧视宽
* **圆锥、圆柱**：一气呵成
* **平面直角坐标系**：有多种预设，图标中两线交点上下左右的长边为实际绘制边。
* **空间直角坐标系**：三轴同时绘制
* **平行线**：从一端到另一端，适合绘制匀强电场的电场线，并针对特殊角度优化
### ppt放映模式
* **翻页**：屏幕中无笔迹时可以多指并排上下滑动翻页，以及控件和键盘方向键翻页
* **保存**：ppt中的笔迹自动保存，还可以移动笔迹文件夹到其他电脑正常使用原笔迹
:warning:ppt模式下打开的内置画板和正常打开的画板一样的，可能需要用户自行关注保存问题:warning:
### 自动墨迹识别（Ink To Shape
1. 需点击“铅笔”打开面板，在面板中启用该功能
2. 尽可能画得标准和规范一些
<!--
2. 正圆内画椭圆，可完成截面圆、同心椭圆（适用于天体轨迹）的识别
3. 可自动完成同心圆、相切圆的识别（内切、外切）
-->
## 技巧 <span id='skill'></span>
* 双击“清屏”按钮即可在清屏的同时隐藏画板，相当于点击“清屏”和“隐藏画板”两个按钮。
* 一不小心擦错了，点击“撤销”按钮即可撤销，再次点击即可恢复。
* 如果需要关闭Ink Canvas画板，可在设置中找到关闭按钮。
* 你可以在 Ink Canvas Better 的 `设置 - 外观选项 - 浮动工具栏显示文字注释` 来获知对应图标的简要说明
<!-- 还没做
* 在任意时刻，点击“黑板”按钮即可进入黑板。
-->
## 感谢 <span id='thank'></span>
特别感谢 [yuwenhui2020](https://github.com/yuwenhui2020) 为 ` Ink Canvas 使用说明` 做出的贡献！
