using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static PVRTexLib.PVRTexLibNative;
using static PVRTexLib.PVRDefine;

namespace PVRTexLib
{
    public unsafe class PVRTextureHeader : IDisposable
    {
        protected void* m_hTextureHeader;
#if NET6_0_OR_GREATER
        public static Func<uint, IntPtr> Alloc = bytes => (IntPtr)NativeMemory.Alloc(bytes);
        public static Action<IntPtr> Free = ptr => NativeMemory.Free((void*)ptr);
#else
        public static Func<uint, IntPtr> Alloc = bytes => Marshal.AllocHGlobal((int)bytes);
        public static Action<IntPtr> Free = ptr => Marshal.FreeHGlobal(ptr);
#endif
        public void* Header => m_hTextureHeader;

        public PVRTextureHeader()
        {
            PVRHeader_CreateParams @params;
            PVRTexLib_SetDefaultTextureHeaderParams(&@params);
            m_hTextureHeader = PVRTexLib_CreateTextureHeader(&@params);
        }

        public PVRTextureHeader(PVRHeader_CreateParams* @params)
        {
            m_hTextureHeader = PVRTexLib_CreateTextureHeader(@params);
        }

        public PVRTextureHeader(in PVRHeader_CreateParams @params)
        {
            fixed (PVRHeader_CreateParams* paramsPtr = &@params)
            {
                m_hTextureHeader = PVRTexLib_CreateTextureHeader(paramsPtr);
            }
        }

        public PVRTextureHeader(ulong pixelFormat, uint width, uint height, uint depth = 1u, uint numMipMaps = 1u, uint numArrayMembers = 1u, uint numFaces = 1u, PVRTexLibColourSpace colourSpace = PVRTexLibColourSpace.PVRTLCS_sRGB, PVRTexLibVariableType channelType = PVRTexLibVariableType.PVRTLVT_UnsignedByteNorm, bool preMultiplied = false)
        {
            PVRHeader_CreateParams @params;
            @params.pixelFormat = pixelFormat;
            @params.width = width;
            @params.height = height;
            @params.depth = depth;
            @params.numMipMaps = numMipMaps;
            @params.numArrayMembers = numArrayMembers;
            @params.numFaces = numFaces;
            @params.colourSpace = colourSpace;
            @params.channelType = channelType;
            @params.preMultiplied = preMultiplied;
            m_hTextureHeader = PVRTexLib_CreateTextureHeader(&@params);
        }

        public PVRTextureHeader(bool _)
        {
            m_hTextureHeader = null;
        }

        public PVRTextureHeader(PVRTextureHeader rhs)
        {
            m_hTextureHeader = PVRTexLib_CopyTextureHeader(rhs.m_hTextureHeader);
        }

        public PVRTextureHeader(in PVRTextureHeader rhs)
        {
            m_hTextureHeader = rhs.m_hTextureHeader;
            rhs.m_hTextureHeader = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            void* hTextureHeader = m_hTextureHeader;
            if (hTextureHeader != null)
            {
                PVRTexLib_DestroyTextureHeader(hTextureHeader);
                m_hTextureHeader = null;
            }
        }

        ~PVRTextureHeader()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public uint GetTextureBitsPerPixel()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureBitsPerPixel(m_hTextureHeader);
            }
            return 0U;
        }

        public uint GetTextureBitsPerPixel(ulong u64PixelFormat)
        {
            return PVRTexLib_GetFormatBitsPerPixel(u64PixelFormat);
        }

        public uint GetTextureChannelCount()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureChannelCount(m_hTextureHeader);
            }
            return 0U;
        }

        public PVRTexLibVariableType GetTextureChannelType()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureChannelType(m_hTextureHeader);
            }
            return PVRTexLibVariableType.PVRTLVT_Invalid;
        }

        public PVRTexLibColourSpace GetColourSpace()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureColourSpace(m_hTextureHeader);
            }
            return PVRTexLibColourSpace.PVRTLCS_NumSpaces;
        }

        public uint GetTextureWidth(uint mipLevel = 0)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureWidth(m_hTextureHeader, mipLevel);
            }
            return 0U;
        }

        public uint GetTextureHeight(uint mipLevel = 0)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureHeight(m_hTextureHeader, mipLevel);
            }
            return 0U;
        }

        public uint GetTextureDepth(uint mipLevel = 0)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureDepth(m_hTextureHeader, mipLevel);
            }
            return 0U;
        }

        public uint GetTextureSize(int mipLevel = PVRTEX_ALLMIPLEVELS, bool allSurfaces = true, bool allFaces = true)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureSize(m_hTextureHeader, mipLevel, allSurfaces, allFaces);
            }
            return 0U;
        }

        public ulong GetTextureDataSize(int mipLevel = PVRTEX_ALLMIPLEVELS, bool allSurfaces = true, bool allFaces = true)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureDataSize(m_hTextureHeader, mipLevel, allSurfaces, allFaces);
            }
            return 0U;
        }

        public void GetTextureOrientation(PVRTexLib_Orientation* result)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_GetTextureOrientation(m_hTextureHeader, result);
            }
            else
            {
                result->x = 0U;
                result->y = 0U;
                result->z = 0U;
            }
        }

        public void GetTextureOrientation(out PVRTexLib_Orientation result)
        {
            if (m_hTextureHeader != null)
            {
                fixed (PVRTexLib_Orientation* resultPtr = &result)
                {
                    PVRTexLib_GetTextureOrientation(m_hTextureHeader, resultPtr);
                }
            }
            else
            {
                result.x = 0U;
                result.y = 0U;
                result.z = 0U;
            }
        }

        public void GetTextureOpenGLFormat(PVRTexLib_OpenGLFormat* result)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_GetTextureOpenGLFormat(m_hTextureHeader, result);
            }
            else
            {
                result->internalFormat = 0U;
                result->format = 0U;
                result->type = 0U;
            }
        }

        public void GetTextureOpenGLFormat(out PVRTexLib_OpenGLFormat result)
        {
            if (m_hTextureHeader != null)
            {
                fixed (PVRTexLib_OpenGLFormat* resultPtr = &result)
                {
                    PVRTexLib_GetTextureOpenGLFormat(m_hTextureHeader, resultPtr);
                }
            }
            else
            {
                result.internalFormat = 0U;
                result.format = 0U;
                result.type = 0U;
            }
        }

        public void GetTextureOpenGLESFormat(PVRTexLib_OpenGLFormat* result)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_GetTextureOpenGLESFormat(m_hTextureHeader, result);
            }
            else
            {
                result->internalFormat = 0U;
                result->format = 0U;
                result->type = 0U;
            }
        }

        public void GetTextureOpenGLESFormat(out PVRTexLib_OpenGLFormat result)
        {
            if (m_hTextureHeader != null)
            {
                fixed (PVRTexLib_OpenGLFormat* resultPtr = &result)
                {
                    PVRTexLib_GetTextureOpenGLESFormat(m_hTextureHeader, resultPtr);
                }
            }
            else
            {
                result.internalFormat = 0U;
                result.format = 0U;
                result.type = 0U;
            }
        }

        public uint GetTextureVulkanFormat()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureVulkanFormat(m_hTextureHeader);
            }
            return 0U;
        }

        public uint GetTextureD3DFormat()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureD3DFormat(m_hTextureHeader);
            }
            return 0U;
        }

        public uint GetTextureDXGIFormat()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureDXGIFormat(m_hTextureHeader);
            }
            return 0U;
        }

        public void GetTextureFormatMinDims(uint* minX, uint* minY, uint* minZ)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_GetTextureFormatMinDims(m_hTextureHeader, minX, minY, minZ);
            }
            else
            {
                *minX = 1U;
                *minY = 1U;
                *minZ = 1U;
            }
        }

        public void GetTextureFormatMinDims(out uint minX, out uint minY, out uint minZ)
        {
            if (m_hTextureHeader != null)
            {
                uint aX, aY, aZ;
                PVRTexLib_GetTextureFormatMinDims(m_hTextureHeader, &aX, &aY, &aZ);
                minX = aX;
                minY = aY;
                minZ = aZ;
            }
            else
            {
                minX = 1U;
                minY = 1U;
                minZ = 1U;
            }
        }

        public (uint minX, uint minY, uint minZ) GetTextureFormatMinDims()
        {
            if (m_hTextureHeader != null)
            {
                uint aX, aY, aZ;
                PVRTexLib_GetTextureFormatMinDims(m_hTextureHeader, &aX, &aY, &aZ);
                return (aX, aY, aZ);
            }
            else
            {
                return (1U, 1U, 1U);
            }
        }

        public void GetPixelFormatMinDims(ulong ui64Format, uint* minX, uint* minY, uint* minZ)
        {
            PVRTexLib_GetPixelFormatMinDims(ui64Format, minX, minY, minZ);
        }

        public void GetPixelFormatMinDims(ulong ui64Format, out uint minX, out uint minY, out uint minZ)
        {
            uint aX, aY, aZ;
            PVRTexLib_GetPixelFormatMinDims(ui64Format, &aX, &aY, &aZ);
            minX = aX;
            minY = aY;
            minZ = aZ;
        }

        public (uint minX, uint minY, uint minZ) GetPixelFormatMinDims(ulong ui64Format)
        {
            uint aX, aY, aZ;
            PVRTexLib_GetPixelFormatMinDims(ui64Format, &aX, &aY, &aZ);
            return (aX, aY, aZ);
        }

        public uint GetTextureMetaDataSize()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureMetaDataSize(m_hTextureHeader);
            }

            return 0U;
        }

        public bool GetTextureIsPreMultiplied()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureIsPreMultiplied(m_hTextureHeader);
            }

            return false;
        }

        public bool GetTextureIsFileCompressed()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureIsFileCompressed(m_hTextureHeader);
            }

            return false;
        }

        public bool GetTextureIsBumpMap()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureIsBumpMap(m_hTextureHeader);
            }

            return false;
        }

        public float GetTextureBumpMapScale()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureBumpMapScale(m_hTextureHeader);
            }

            return 0.0f;
        }

        public uint GetNumTextureAtlasMembers()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetNumTextureAtlasMembers(m_hTextureHeader);
            }

            return 0U;
        }

        public float* GetTextureAtlasData(uint* count)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureAtlasData(m_hTextureHeader, count);
            }

            *count = 0U;
            return null;
        }

        public float* GetTextureAtlasData(out uint count)
        {
            if (m_hTextureHeader != null)
            {
                uint aCount;
                float* data = PVRTexLib_GetTextureAtlasData(m_hTextureHeader, &aCount);
                count = aCount;
                return data;
            }

            count = 0U;
            return null;
        }

        public uint GetTextureNumMipMapLevels()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureNumMipMapLevels(m_hTextureHeader);
            }

            return 0U;
        }

        public uint GetTextureNumFaces()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureNumFaces(m_hTextureHeader);
            }

            return 0U;
        }

        public uint GetTextureNumArrayMembers()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTextureNumArrayMembers(m_hTextureHeader);
            }

            return 0U;
        }

        public string GetTextureCubeMapOrder()
        {
            if (m_hTextureHeader != null)
            {
                StringBuilder sb = new StringBuilder(8);
                PVRTexLib_GetTextureCubeMapOrder(m_hTextureHeader, sb);
                return sb.ToString();
            }
            return null;
        }

        public string GetTextureBumpMapOrder()
        {
            if (m_hTextureHeader != null)
            {
                StringBuilder sb = new StringBuilder(8);
                PVRTexLib_GetTextureBumpMapOrder(m_hTextureHeader, sb);
                return sb.ToString();
            }
            return null;
        }

        public ulong GetTexturePixelFormat()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_GetTexturePixelFormat(m_hTextureHeader);
            }

            return (ulong)(PVRTexLibPixelFormat.PVRTLPF_NumCompressedPFs);
        }

        public bool TextureHasPackedChannelData()
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_TextureHasPackedChannelData(m_hTextureHeader);
            }

            return false;
        }

        public bool IsPixelFormatCompressed()
        {
            return (GetTexturePixelFormat() & PVRTEX_PFHIGHMASK) == 0;
        }

        public void SetTextureChannelType(PVRTexLibVariableType type)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureChannelType(m_hTextureHeader, type);
            }
        }

        public void SetTextureColourSpace(PVRTexLibColourSpace colourSpace)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureColourSpace(m_hTextureHeader, colourSpace);
            }
        }

        public bool SetTextureD3DFormat(uint d3dFormat)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_SetTextureD3DFormat(m_hTextureHeader, d3dFormat);
            }

            return false;
        }

        public bool SetTextureDXGIFormat(uint dxgiFormat)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_SetTextureDXGIFormat(m_hTextureHeader, dxgiFormat);
            }

            return false;
        }

        public bool SetTextureOGLFormat(PVRTexLib_OpenGLFormat* oglFormat)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_SetTextureOGLFormat(m_hTextureHeader, oglFormat);
            }

            return false;
        }

        public bool SetTextureOGLFormat(in PVRTexLib_OpenGLFormat oglFormat)
        {
            if (m_hTextureHeader != null)
            {
                fixed (PVRTexLib_OpenGLFormat* ptr = &oglFormat)
                {
                    return PVRTexLib_SetTextureOGLFormat(m_hTextureHeader, ptr);
                }
            }

            return false;
        }

        public bool SetTextureOGLESFormat(PVRTexLib_OpenGLFormat* oglesFormat)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_SetTextureOGLESFormat(m_hTextureHeader, oglesFormat);
            }

            return false;
        }

        public bool SetTextureOGLESFormat(in PVRTexLib_OpenGLFormat oglesFormat)
        {
            if (m_hTextureHeader != null)
            {
                fixed (PVRTexLib_OpenGLFormat* ptr = &oglesFormat)
                {
                    return PVRTexLib_SetTextureOGLESFormat(m_hTextureHeader, ptr);
                }
            }

            return false;
        }

        public bool SetTextureVulkanFormat(uint vulkanFormat)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_SetTextureVulkanFormat(m_hTextureHeader, vulkanFormat);
            }

            return false;
        }

        public void SetTexturePixelFormat(ulong format)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTexturePixelFormat(m_hTextureHeader, format);
            }
        }

        public void SetTextureWidth(uint width)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureWidth(m_hTextureHeader, width);
            }
        }

        public void SetTextureHeight(uint height)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureHeight(m_hTextureHeader, height);
            }
        }

        public void SetTextureDepth(uint depth)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureDepth(m_hTextureHeader, depth);
            }
        }

        public void SetTextureNumArrayMembers(uint numMembers)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureNumArrayMembers(m_hTextureHeader, numMembers);
            }
        }

        public void SetTextureNumMIPLevels(uint numMIPLevels)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureNumMIPLevels(m_hTextureHeader, numMIPLevels);
            }
        }

        public void SetTextureNumFaces(uint numFaces)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureNumFaces(m_hTextureHeader, numFaces);
            }
        }

        public void SetTextureOrientation(PVRTexLib_Orientation* orientation)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureOrientation(m_hTextureHeader, orientation);
            }
        }

        public void SetTextureOrientation(in PVRTexLib_Orientation orientation)
        {
            if (m_hTextureHeader != null)
            {
                fixed (PVRTexLib_Orientation* ptr = &orientation)
                {
                    PVRTexLib_SetTextureOrientation(m_hTextureHeader, ptr);
                }
            }
        }

        public void SetTextureIsFileCompressed(bool isFileCompressed)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureIsFileCompressed(m_hTextureHeader, isFileCompressed);
            }
        }

        public void SetTextureIsPreMultiplied(bool isPreMultiplied)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureIsPreMultiplied(m_hTextureHeader, isPreMultiplied);
            }
        }

        public void GetTextureBorder(uint* borderWidth, uint* borderHeight, uint* borderDepth)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_GetTextureBorder(m_hTextureHeader, borderWidth, borderHeight, borderDepth);
            }
            else
            {
                *borderWidth = 0U;
                *borderHeight = 0U;
                *borderDepth = 0U;
            }
        }

        public void GetTextureBorder(out uint borderWidth, out uint borderHeight, out uint borderDepth)
        {
            if (m_hTextureHeader != null)
            {
                uint w, h, d;
                PVRTexLib_GetTextureBorder(m_hTextureHeader, &w, &h, &d);
                borderWidth = w;
                borderHeight = h;
                borderDepth = d;
            }
            else
            {
                borderWidth = 0U;
                borderHeight = 0U;
                borderDepth = 0U;
            }
        }

        public (uint borderWidth, uint borderHeight, uint borderDepth) GetTextureBorder()
        {
            if (m_hTextureHeader != null)
            {
                uint w, h, d;
                PVRTexLib_GetTextureBorder(m_hTextureHeader, &w, &h, &d);
                return (w, h, d);
            }
            else
            {
                return (0U, 0U, 0U);
            }
        }

        public bool GetMetaDataBlock(uint key, PVRTexLib_MetaDataBlock* dataBlock, uint devFOURCC = PVRTEX_CURR_IDENT)
        {
            if (m_hTextureHeader != null)
            {
                if (PVRTexLib_GetMetaDataBlock(m_hTextureHeader, devFOURCC, key, dataBlock, Alloc))
                {
                    return true;
                }
            }

            dataBlock->Reset();
            return false;
        }

        public bool GetMetaDataBlock(uint key, out PVRTexLib_MetaDataBlock dataBlock, uint devFOURCC = PVRTEX_CURR_IDENT)
        {
            PVRTexLib_MetaDataBlock temp = new PVRTexLib_MetaDataBlock();
            temp.Reset();
            if (m_hTextureHeader != null)
            {
                if (PVRTexLib_GetMetaDataBlock(m_hTextureHeader, devFOURCC, key, &temp, Alloc))
                {
                    dataBlock = temp;
                    return true;
                }
            }
            dataBlock = temp;
            return false;
        }

        public bool TextureHasMetaData(uint key, uint devFOURCC = PVRTEX_CURR_IDENT)
        {
            if (m_hTextureHeader != null)
            {
                return PVRTexLib_TextureHasMetaData(m_hTextureHeader, devFOURCC, key);
            }

            return false;
        }

        public void SetTextureBumpMap(float bumpScale, string bumpOrder)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureBumpMap(m_hTextureHeader, bumpScale, bumpOrder);
            }
        }

        public void SetTextureAtlas(float* atlasData, uint dataSize)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureAtlas(m_hTextureHeader, atlasData, dataSize);
            }
        }

        public void SetTextureCubeMapOrder(string cubeMapOrder)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureCubeMapOrder(m_hTextureHeader, cubeMapOrder);
            }
        }

        public void SetTextureBorder(uint borderWidth, uint borderHeight, uint borderDepth)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_SetTextureBorder(m_hTextureHeader, borderWidth, borderHeight, borderDepth);
            }
        }

        public void AddMetaData(PVRTexLib_MetaDataBlock* dataBlock)
        {
            if (m_hTextureHeader != null && dataBlock->u32DataSize != 0)
            {
                PVRTexLib_AddMetaData(m_hTextureHeader, dataBlock);
            }
        }

        public void AddMetaData(in PVRTexLib_MetaDataBlock dataBlock)
        {
            if (m_hTextureHeader != null && dataBlock.u32DataSize != 0)
            {
                fixed (PVRTexLib_MetaDataBlock* ptr = &dataBlock)
                {
                    PVRTexLib_AddMetaData(m_hTextureHeader, ptr);
                }
            }
        }

        private void RemoveMetaData(uint key, uint devFOURCC = PVRTEX_CURR_IDENT)
        {
            if (m_hTextureHeader != null)
            {
                PVRTexLib_RemoveMetaData(m_hTextureHeader, devFOURCC, key);
            }
        }
    }

    public unsafe class PVRTexture : PVRTextureHeader
    {
        protected void* m_hTexture;

        public PVRTexture() : base(false)
        {
            m_hTexture = null;
        }

        public PVRTexture(in PVRTextureHeader header, void* textureData) : base(false)
        {
            m_hTexture = PVRTexLib_CreateTexture(header.Header, textureData);
            m_hTextureHeader = PVRTexLib_GetTextureHeaderW(m_hTexture);
        }

        public PVRTexture(string filePath) : base(false)
        {
            m_hTexture = PVRTexLib_CreateTextureFromFile(filePath);
            if (m_hTexture != null)
            {
                m_hTextureHeader = PVRTexLib_GetTextureHeaderW(m_hTexture);
            }
            else
            {
                throw new Exception("Couldn't load texture: " + filePath);
            }
        }

        public PVRTexture(void* data) : base(false)
        {
            m_hTexture = PVRTexLib_CreateTextureFromData(data);
            if (m_hTexture != null)
            {
                m_hTextureHeader = PVRTexLib_GetTextureHeaderW(m_hTexture);
            }
            else
            {
                throw new Exception("Provided pointer to texture data is invalid");
            }
        }

        public PVRTexture(PVRTexture rhs) : base(false)
        {
            m_hTexture = PVRTexLib_CopyTexture(rhs.m_hTexture);
            m_hTextureHeader = PVRTexLib_GetTextureHeaderW(m_hTexture);
        }

        public PVRTexture(in PVRTexture rhs) : base(false)
        {
            m_hTexture = rhs.m_hTexture;
            m_hTextureHeader = rhs.m_hTextureHeader;
            rhs.m_hTextureHeader = null;
            rhs.m_hTexture = null;
        }

        ~PVRTexture()
        {
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            Destroy();
            base.Dispose(disposing);
        }

        protected void Destroy()
        {
            void* hTexture = m_hTexture;
            if (hTexture != null)
            {
                PVRTexLib_DestroyTexture(hTexture);
                m_hTexture = null;
                m_hTextureHeader = null;
            }
        }

        public void* GetTextureDataPointer(uint MIPLevel = 0U, uint arrayMember = 0U, uint faceNumber = 0U, uint ZSlice = 0U)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_GetTextureDataPtr(m_hTexture, MIPLevel, arrayMember, faceNumber, ZSlice);
            }

            return null;
        }

        public void* GetTextureDataConstPointer(uint MIPLevel = 0U, uint arrayMember = 0U, uint faceNumber = 0U, uint ZSlice = 0U)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_GetTextureDataConstPtr(m_hTexture, MIPLevel, arrayMember, faceNumber, ZSlice);
            }

            return null;
        }

        public void AddPaddingMetaData(uint padding)
        {
            if (m_hTexture != null)
            {
                PVRTexLib_AddPaddingMetaData(m_hTexture, padding);
            }
        }

        public bool SaveToFile(string filePath)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_SaveTextureToFile(m_hTexture, filePath);
            }

            return false;
        }

        public bool SaveTextureToMemory(PVRTexLibFileContainerType fileType, void* privateData, ulong* outSize, Func<IntPtr, ulong, IntPtr> pfnRealloc)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_SaveTextureToMemory(m_hTexture, fileType, privateData, outSize, pfnRealloc);
            }

            return false;
        }

        public bool SaveTextureToMemory(PVRTexLibFileContainerType fileType, void* privateData, out ulong outSize, Func<IntPtr, ulong, IntPtr> pfnRealloc)
        {
            if (m_hTexture != null)
            {
                ulong oSize;
                bool status = PVRTexLib_SaveTextureToMemory(m_hTexture, fileType, privateData, &oSize, pfnRealloc);
                outSize = oSize;
                return status;
            }

            outSize = 0u;
            return false;
        }

        // 还有两个SaveTextureToMemory没实现 懒了

        public bool SaveToFile(string filePath, PVRTexLibLegacyApi api)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_SaveTextureToLegacyPVRFile(m_hTexture, filePath, api);
            }

            return false;
        }

        public bool SaveSurfaceToImageFile(string filePath, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint ZSlice = 0U)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_SaveSurfaceToImageFile(m_hTexture, filePath, MIPLevel, arrayMember, face, ZSlice);
            }

            return false;
        }

        public bool IsTextureMultiPart()
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_IsTextureMultiPart(m_hTexture);
            }

            return false;
        }

        public List<PVRTexture> GetTextureParts()
        {
            List<PVRTexture> textures = new List<PVRTexture>();

            if (m_hTexture != null)
            {
                uint count;
                void*[] handles;
                PVRTexLib_GetTextureParts(m_hTexture, null, &count);
                handles = new void*[count];
                fixed (void** ptr = &handles[0])
                {
                    PVRTexLib_GetTextureParts(m_hTexture, ptr, &count);
                }
                foreach (var handle in handles)
                {
                    PVRTexture tex = new PVRTexture();
                    tex.m_hTexture = handle;
                    tex.m_hTextureHeader = PVRTexLib_GetTextureHeaderW(handle);
                    textures.Add(tex);
                }
            }

            return textures;
        }

        public bool Resize(uint newWidth, uint newHeight, uint newDepth, PVRTexLibResizeMode resizeMode)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_ResizeTexture(m_hTexture, newWidth, newHeight, newDepth, resizeMode);
            }

            return false;
        }

        public bool ResizeCanvas(uint newWidth, uint newHeight, uint newDepth, int xOffset, int yOffset, int zOffset)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_ResizeTextureCanvas(m_hTexture, newWidth, newHeight, newDepth, xOffset, yOffset, zOffset);
            }

            return false;
        }

        public bool Rotate(PVRTexLibAxis rotationAxis, bool forward)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_RotateTexture(m_hTexture, rotationAxis, forward);
            }

            return false;
        }

        public bool Flip(PVRTexLibAxis flipDirection)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_FlipTexture(m_hTexture, flipDirection);
            }

            return false;
        }

        public bool Border(uint borderX, uint borderY, uint borderZ)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_BorderTexture(m_hTexture, borderX, borderY, borderZ);
            }

            return false;
        }

        public bool PreMultiplyAlpha()
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_PreMultiplyAlpha(m_hTexture);
            }

            return false;
        }

        public bool Bleed()
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_Bleed(m_hTexture);
            }

            return false;
        }

        public bool SetChannels(uint numChannelSets, PVRTexLibChannelName* channels, uint* pValues)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_SetTextureChannels(m_hTexture, numChannelSets, channels, pValues);
            }

            return false;
        }

        public bool SetChannels(PVRTexLibChannelName[] channels, uint[] pValues)
        {
            if (m_hTexture != null)
            {
                fixed (PVRTexLibChannelName* channelsPtr = &channels[0])
                {
                    fixed (uint* pValuesPtr = &pValues[0])
                    {
                        return PVRTexLib_SetTextureChannels(m_hTexture, (uint)Math.Min(channels.Length, pValues.Length), channelsPtr, pValuesPtr);
                    }
                }
            }

            return false;
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        public bool SetChannels(ReadOnlySpan<PVRTexLibChannelName> channels, ReadOnlySpan<uint> pValues)
        {
            if (m_hTexture != null)
            {
                fixed (PVRTexLibChannelName* channelsPtr = &channels[0])
                {
                    fixed (uint* pValuesPtr = &pValues[0])
                    {
                        return PVRTexLib_SetTextureChannels(m_hTexture, (uint)Math.Min(channels.Length, pValues.Length), channelsPtr, pValuesPtr);
                    }
                }
            }

            return false;
        }
#endif

        public bool SetChannels(uint numChannelSets, PVRTexLibChannelName* channels, float* pValues)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_SetTextureChannelsFloat(m_hTexture, numChannelSets, channels, pValues);
            }

            return false;
        }

        public bool SetChannels(PVRTexLibChannelName[] channels, float[] pValues)
        {
            if (m_hTexture != null)
            {
                fixed (PVRTexLibChannelName* channelsPtr = &channels[0])
                {
                    fixed (float* pValuesPtr = &pValues[0])
                    {
                        return PVRTexLib_SetTextureChannelsFloat(m_hTexture, (uint)Math.Min(channels.Length, pValues.Length), channelsPtr, pValuesPtr);
                    }
                }
            }

            return false;
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        public bool SetChannels(ReadOnlySpan<PVRTexLibChannelName> channels, ReadOnlySpan<float> pValues)
        {
            if (m_hTexture != null)
            {
                fixed (PVRTexLibChannelName* channelsPtr = &channels[0])
                {
                    fixed (float* pValuesPtr = &pValues[0])
                    {
                        return PVRTexLib_SetTextureChannelsFloat(m_hTexture, (uint)Math.Min(channels.Length, pValues.Length), channelsPtr, pValuesPtr);
                    }
                }
            }

            return false;
        }
#endif

        public bool CopyChannels(PVRTexture sourceTexture, uint numChannelCopies, PVRTexLibChannelName* destinationChannels, PVRTexLibChannelName* sourceChannels)
        {
            if (m_hTexture != null && sourceTexture.m_hTexture != null)
            {
                return PVRTexLib_CopyTextureChannels(m_hTexture, sourceTexture.m_hTexture, numChannelCopies, destinationChannels, sourceChannels);
            }

            return false;
        }

        public bool CopyChannels(PVRTexture sourceTexture, PVRTexLibChannelName[] destinationChannels, PVRTexLibChannelName[] sourceChannels)
        {
            if (m_hTexture != null && sourceTexture.m_hTexture != null)
            {
                fixed (PVRTexLibChannelName* destinationChannelsPtr = destinationChannels, sourceChannelsPtr = sourceChannels)
                {
                    return PVRTexLib_CopyTextureChannels(m_hTexture, sourceTexture.m_hTexture, (uint)Math.Min(destinationChannels.Length, sourceChannels.Length), destinationChannelsPtr, sourceChannelsPtr);
                }
            }

            return false;
        }

#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
        public bool CopyChannels(PVRTexture sourceTexture, ReadOnlySpan<PVRTexLibChannelName> destinationChannels, ReadOnlySpan<PVRTexLibChannelName> sourceChannels)
        {
            if (m_hTexture != null && sourceTexture.m_hTexture != null)
            {
                fixed (PVRTexLibChannelName* destinationChannelsPtr = destinationChannels, sourceChannelsPtr = sourceChannels)
                {
                    return PVRTexLib_CopyTextureChannels(m_hTexture, sourceTexture.m_hTexture, (uint)Math.Min(destinationChannels.Length, sourceChannels.Length), destinationChannelsPtr, sourceChannelsPtr);
                }
            }

            return false;
        }
#endif

        public bool GenerateNormalMap(float fScale, string channelOrder)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_GenerateNormalMap(m_hTexture, fScale, channelOrder);
            }

            return false;
        }

        public bool GenerateMIPMaps(PVRTexLibResizeMode filterMode, int mipMapsToDo = PVRTEX_ALLMIPLEVELS)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_GenerateMIPMaps(m_hTexture, filterMode, mipMapsToDo);
            }

            return false;
        }

        public bool ColourMIPMaps()
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_ColourMIPMaps(m_hTexture);
            }

            return false;
        }

        public bool Transcode(ulong pixelFormat, PVRTexLibVariableType channelType, PVRTexLibColourSpace colourspace, PVRTexLibCompressorQuality quality = PVRTexLibCompressorQuality.PVRTLCQ_PVRTCNormal, bool doDither = false, float maxRange = 1.0f, uint maxThreads = 0U)
        {
            if (m_hTexture != null)
            {

                PVRTexLib_TranscoderOptions options;
                options.sizeofStruct = (uint)sizeof(PVRTexLib_TranscoderOptions);
                options.pixelFormat = pixelFormat;
#if NET8_0_OR_GREATER
                options.channelType[0] = options.channelType[1] = options.channelType[2] = options.channelType[3] = channelType;
#else
                options.channelType0 = options.channelType1 = options.channelType2 = options.channelType3 = channelType;
#endif
                options.colourspace = colourspace;
                options.quality = quality;
                options.doDither = doDither;
                options.maxRange = maxRange;
                options.maxThreads = maxThreads;
                return PVRTexLib_TranscodeTexture(m_hTexture, &options);
            }

            return false;
        }

        public bool Transcode(in PVRTexLib_TranscoderOptions options)
        {
            if (m_hTexture != null)
            {
                fixed (PVRTexLib_TranscoderOptions* ptr = &options)
                {
                    return PVRTexLib_TranscodeTexture(m_hTexture, ptr);
                }
            }

            return false;
        }

        public bool Transcode(PVRTexLib_TranscoderOptions* options)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_TranscodeTexture(m_hTexture, options);
            }

            return false;
        }

        public bool Decompress(uint maxThreads = 0U)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_Decompress(m_hTexture, maxThreads);
            }

            return false;
        }

        public bool EquiRectToCubeMap(PVRTexLibResizeMode filter)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_EquiRectToCubeMap(m_hTexture, filter);
            }

            return false;
        }

        public bool GenerateDiffuseIrradianceCubeMap(uint sampleCount, uint mapSize)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_GenerateDiffuseIrradianceCubeMap(m_hTexture, sampleCount, mapSize);
            }

            return false;
        }

        public bool GeneratePreFilteredSpecularCubeMap(
            uint sampleCount,
            uint mapSize,
            uint numMipLevelsToDiscard,
            bool zeroRoughnessIsExternal)
        {
            if (m_hTexture != null)
            {
                return PVRTexLib_GeneratePreFilteredSpecularCubeMap(m_hTexture, sampleCount, mapSize, numMipLevelsToDiscard, zeroRoughnessIsExternal);
            }

            return false;
        }

        public bool MaxDifference(PVRTexture texture, PVRTexLib_ErrorMetrics* metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                return PVRTexLib_MaxDifference(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, metrics);
            }

            return false;
        }

        public bool MaxDifference(PVRTexture texture, out PVRTexLib_ErrorMetrics metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            PVRTexLib_ErrorMetrics temp = new PVRTexLib_ErrorMetrics();
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                bool succeed = PVRTexLib_MaxDifference(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, &temp);
                metrics = temp;
                return succeed;
            }
            metrics = temp;
            return false;
        }

        public bool MeanError(PVRTexture texture, PVRTexLib_ErrorMetrics* metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                return PVRTexLib_MeanError(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, metrics);
            }

            return false;
        }

        public bool MeanError(PVRTexture texture, out PVRTexLib_ErrorMetrics metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            PVRTexLib_ErrorMetrics temp = new PVRTexLib_ErrorMetrics();
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                bool succeed = PVRTexLib_MeanError(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, &temp);
                metrics = temp;
                return succeed;
            }
            metrics = temp;
            return false;
        }

        public bool MeanSquaredError(PVRTexture texture, PVRTexLib_ErrorMetrics* metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                return PVRTexLib_MeanSquaredError(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, metrics);
            }

            return false;
        }

        public bool MeanSquaredError(PVRTexture texture, out PVRTexLib_ErrorMetrics metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            PVRTexLib_ErrorMetrics temp = new PVRTexLib_ErrorMetrics();
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                bool succeed = PVRTexLib_MeanSquaredError(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, &temp);
                metrics = temp;
                return succeed;
            }
            metrics = temp;
            return false;
        }

        public bool RootMeanSquaredError(PVRTexture texture, PVRTexLib_ErrorMetrics* metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                return PVRTexLib_RootMeanSquaredError(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, metrics);
            }

            return false;
        }

        public bool RootMeanSquaredError(PVRTexture texture, out PVRTexLib_ErrorMetrics metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            PVRTexLib_ErrorMetrics temp = new PVRTexLib_ErrorMetrics();
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                bool succeed = PVRTexLib_RootMeanSquaredError(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, &temp);
                metrics = temp;
                return succeed;
            }
            metrics = temp;
            return false;
        }

        public bool StandardDeviation(PVRTexture texture, PVRTexLib_ErrorMetrics* metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                return PVRTexLib_StandardDeviation(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, metrics);
            }

            return false;
        }

        public bool StandardDeviation(PVRTexture texture, out PVRTexLib_ErrorMetrics metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            PVRTexLib_ErrorMetrics temp = new PVRTexLib_ErrorMetrics();
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                bool succeed = PVRTexLib_StandardDeviation(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, &temp);
                metrics = temp;
                return succeed;
            }
            metrics = temp;
            return false;
        }

        public bool PeakSignalToNoiseRatio(PVRTexture texture, PVRTexLib_ErrorMetrics* metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                return PVRTexLib_PeakSignalToNoiseRatio(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, metrics);
            }

            return false;
        }

        public bool PeakSignalToNoiseRatio(PVRTexture texture, out PVRTexLib_ErrorMetrics metrics, uint MIPLevel = 0U, uint arrayMember = 0U, uint face = 0U, uint zSlice = 0U)
        {
            PVRTexLib_ErrorMetrics temp = new PVRTexLib_ErrorMetrics();
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                bool succeed = PVRTexLib_PeakSignalToNoiseRatio(m_hTexture, texture.m_hTexture, MIPLevel, arrayMember, face, zSlice, &temp);
                metrics = temp;
                return succeed;
            }
            metrics = temp;
            return false;
        }

        public bool ColourDiff(PVRTexture texture, out PVRTexture textureResult, float multiplier = 1.0f, PVRTexLibColourDiffMode mode = PVRTexLibColourDiffMode.PVRTLCDM_Abs)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                void* result = null;
                if (PVRTexLib_ColourDiff(m_hTexture, texture.m_hTexture, &result, multiplier, mode))
                {
                    textureResult = new PVRTexture(result);
                    return true;
                }
            }
            textureResult = null;
            return false;
        }

        public bool ToleranceDiff(PVRTexture texture, out PVRTexture textureResult, float tolerance = 0.1f)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                void* result = null;
                if (PVRTexLib_ToleranceDiff(m_hTexture, texture.m_hTexture, &result, tolerance))
                {
                    textureResult = new PVRTexture(result);
                    return true;
                }
            }
            textureResult = null;
            return false;
        }

        public bool BlendDiff(PVRTexture texture, out PVRTexture textureResult, float blendFactor = 0.5f)
        {
            if (m_hTexture != null && texture.m_hTexture != null)
            {
                void* result = null;
                if (PVRTexLib_BlendDiff(m_hTexture, texture.m_hTexture, &result, blendFactor))
                {
                    textureResult = new PVRTexture(result);
                    return true;
                }
            }
            textureResult = null;
            return false;
        }
    }
}
