# CMakeList.txt : CMake project for Release, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

project ("Release")

# Add source to this project's executable.
add_executable (Release "Release.cpp" "Release.h")
set(ROOT ${CMAKE_SOURCE_DIR}/libs)

target_link_libraries(Release PUBLIC ${ROOT}/sfml-audio.lib)
target_link_libraries(Release PUBLIC ${ROOT}/sfml-graphics.lib)
target_link_libraries(Release PUBLIC ${ROOT}/sfml-network.lib)
target_link_libraries(Release PUBLIC ${ROOT}/sfml-system.lib)
target_link_libraries(Release PUBLIC  ${ROOT}/sfml-window.lib)

set(WTF "C:/Users/muham/source/repos/projects/clangs/Cartographer/Cartographer/libs/imgui/CmakeLists.txt")
include("C:/Users/muham/source/repos/projects/clangs/Cartographer/Cartographer/cmakelists.txt")
target_link_libraries(Cartographer PRIVATE Cartographer)
set(SFML_INCLUDE_DIRS "C:/Users/muham/source/repos/third_party/SFML/SFML-2.5.1-windows-vc15-32-bit/SFML-2.5.1/include")
target_include_directories(Release PUBLIC ${SFML_INCLUDE_DIRS})