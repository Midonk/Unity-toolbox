# Multi Builder

**v0.1.1**


## Description

The main purpose of the *Multi Builder* is to allow you to build multiple players with a single click.

Simple of use and easy to manipulate, the *Multi Builder* provides you a settings asset to parametrize *build threads*. Each *build thread* is meant to get built one after the other.


## Installation

Simply modify your manifest.json file found at /PROJECTNAME/Packages/manifest.json by including the following line

```json
{
	"dependencies": {
		...
		"com.chaos-entertainment.multibuilder": "https://github.com/Midonk/MultiBuilder.git",
		"com.chaos-entertainment.tools": "https://github.com/Midonk/Tool-Bases.git",
		...
	}
}
```


## Quick start

Once you installed the package, you should see a new *menu item* in ***Tools / Multibuilder***.
You have to initialize the system by creating settings asset. To do so, just click on the any new menu item.

Now the settings asset is created, everything you need is to parametrize the settings to be able to build any number of players you'd like.

Finally, to start the builds, you have the choice:
- Use the *menu item* at ***Tools / Multibuilder / Build***.
- Use the *Build threads* button visible in the *Multi Builder* settings.


## Features

- Multiple configurable *build threads*
- Auto *Build Platform* switch


## Known issues

- You have to suffix the output file with the appropriate extension (if needed) to be able to read it directly


## Possible improvements

- Mimic the *Build settings* parameters
- Adressable support