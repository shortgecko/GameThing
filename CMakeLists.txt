# CMakeList.txt : CMake project for Cartographer, include source and define
# project specific logic here.

cmake_minimum_required (VERSION 3.8)

project (Cartographer)


add_library (Cartographer 
	            Cartographer.h
				source/math/vec2.cpp
				source/math/rect.cpp
				source/graphics/color.cpp 
				source/logger.cpp
				source/app.cpp
				source/renderer.cpp
				include/imgui_backend.h)

set(ROOT ${CMAKE_CURRENT_SOURCE_DIR})

target_include_directories(Cartographer
	PUBLIC
	$<BUILD_INTERFACE:${ROOT}/include>
	PRIVATE
	$<BUILD_INTERFACE:${ROOT}/source>
	PUBLIC LIBS
	)


#dear imgui
include(${WTF})
target_link_libraries(Cartographer PRIVATE imgui)
