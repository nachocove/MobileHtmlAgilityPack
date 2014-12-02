XAMARIN_STUDIO_DIR := /Applications/Xamarin\ Studio.app/Contents/MacOS
MDTOOL := $(XAMARIN_STUDIO_DIR)/mdtool

all:
	$(MDTOOL) build --target:Build MobileHtmlAgilityPack.sln

clean:
	$(MDTOOL) build --target:Clean MobileHtmlAgilityPack.sln
	rm -fr obj bin Test.Mac/bin Test.Mac/obj

test: all
	find . -name "*.html" -exec mono Test.Mac/bin/Debug/Test.Mac.exe {} +