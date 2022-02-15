# Debug Menu
**v1.0**

## Description

*DebugMenu* aims to be an easy and efficient tool to allow its user to quickly setup an in-game debug menu.

To do so, the package uses attributes that derive from `DebugMenuAttribute` to determine public functions that can be used to build a menu.

The menu will be built following the architecture you provide to the attributes.

Then, using a single `DebugMenuHandler` component, the package links attributes types to custom UI display.


## Quick Start

First, you have to setup the *DebugMenuScene* by adding it to the *Build settings*. You can find a simple *DebugMenuScene* in the provided scenes of the package.

Next, in the scenes where you wish to be able to use the *DebugMenu*, add the provided *DebugMenuHandle* prefab. You will surely want to setup the `InputSequencer` that will trigger the menu display. By default, in play mode, just press 3 times 'W'.

Finally, add any `DebugMenu` attribute on top of any of your public function in any script you'd like.

Test it !


## How does it works ?

Once the *DebugMenuScene* get loaded, the system will walk through all your assemblies to fetch your methods headed with a `DebugMenuAttribute` and the path (and eventually other paramters) you provided.

Then an architecture will be created based on all the paths you passed in the attributes.

The menu building will now be triggered and request the creation of the root of your architecture.

Depending of the method behind the root button's paths, the `DebugMenuHandler` will use its `ButtonLink`s to define the display. Now we have the display, the system just build the required `DebugMenuButton`.

If the path has already been built, it will be reused.


## Menu navigation

You may use your mouse to navigate through the menu but a more convenient way is to use your keyboard:
- **Select up & down**: vertical axis or up and down arrow keys
- **Execute**: space or return key on selected `MenuButton`
- **Edit handles (< ... >)**: horizontal axis or left and right arrow keys
- **Edit number handle manually**: press any numeric key when `MenuNumberButton` is selected
- **Navigate to (on `MenuNavigationButton`)**: space or return or right arrow key
- **Go back**: escape key


## How to customize your debug menu ?

If you added a new attribute inheriting from `DebugMenuAttribute`, you should probably update the attribute references by refreshing the settings located in *Assets/Editor/Settings/DebugMenuSettings*. Press the big button 'Refresh' and the new references should appear


## Features

- Sample custom attributes
- Sample custom UI display
- Reference logger tool
- A quick access menu


## Customization

- Create your own custom attributes
- Create your own custom UI display
- Create your own custom debug menu


## Limitations

- Debug Menu only support functions with a miximum of one parameter type for now
- The debug menu works with `OnGUI` and `KeyCode` so it don't support the new *Input System*


## Possible improvement

- Navigation with controller
- Support function using more than one parameter