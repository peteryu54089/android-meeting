/*****************************************************************************
 * eventfd.c
 *****************************************************************************
 * Copyright 穢 2012 Rafa禱l Carr矇
 * Copyright 穢 2012 VLC authors and VideoLAN
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston MA 02110-1301, USA.
 *****************************************************************************/

#include <sys/linux-syscalls.h>
#include <sys/syscall.h>
#include <errno.h>

int eventfd(unsigned int initval, int flags)
{
    return syscall(__NR_eventfd2, initval, flags);
}
