﻿// Copyright (c) HQ.IO Corporation. All rights reserved.
// Licensed under the Reciprocal Public License, Version 1.5. See LICENSE.md in the project root for license terms.

using System;
using System.ComponentModel.DataAnnotations;

namespace HQ.Rosetta
{
	public interface IObject
	{
		[Key] long Id { get; }
		DateTimeOffset CreatedAt { get; }
	}
}