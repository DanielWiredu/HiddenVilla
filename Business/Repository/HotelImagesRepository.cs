using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Repository
{
    public class HotelImagesRepository : IHotelImagesRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public HotelImagesRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
        {
            var image = _mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
            await _db.HotelRoomsImages.AddAsync(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
        {
            var image = await _db.HotelRoomsImages.FindAsync(imageId);
            _db.HotelRoomsImages.Remove(image);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl)
        {
            var allImages = await _db.HotelRoomsImages.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            _db.HotelRoomsImages.Remove(allImages);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var imageList = await _db.HotelRoomsImages.Where(x => x.RoomId == roomId).ToListAsync();
            _db.HotelRoomsImages.RemoveRange(imageList);
            return await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelRoomImageDTO>> GetAllHotelRoomImages(int roomId)
        {
            return _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDTO>>(
            await _db.HotelRoomsImages.Where(x => x.RoomId == roomId).ToListAsync());
        }
    }
}
