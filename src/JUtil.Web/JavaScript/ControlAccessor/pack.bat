set pack=%1

set dependences= Impl\AspNetControlHelperManager.js, Impl\ControlAccessorImpl\BulletedListHelper.js, Impl\ControlAccessorImpl\ButtonHelper.js, Impl\ControlAccessorImpl\CheckBoxListHelper.js, Impl\ControlAccessorImpl\ddComboHelper.js, Impl\ControlAccessorImpl\DropDownListHelper.js, Impl\ControlAccessorImpl\HiddenFieldHelper.js, Impl\ControlAccessorImpl\LabelHelper.js, Impl\ControlAccessorImpl\RadioButtonListHelper.js, Impl\ControlAccessorImpl\TextBoxHelper.js, ControlAccessor.js

for %%A in (%dependences%) do type %%A >> %pack%