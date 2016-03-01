慕课英雄（Mooc Hero）
=====
基于安卓平台的第一人称射击游戏。
实验室项目，Cousera某未上线课程的Project。

细节备忘
=====
 * Cross Platform Input插件包中，TouchPad的代码被修改，以放大触摸操作。
 * PlayerAttack代码中，仅仅处理了UnityEditor和Android平台的输入事件，对Windows本地发布的程序未处理，如果发布为exe，会出现无法射击的情况。