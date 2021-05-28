bool collision(float x_move_amount, float y_move_amount)
{
    auto hitbox = hitbox(entity->position.x + x_move_amount, entity->position.y + y_move_amount,
     entity->hitbox.width, entity->hitbox.height);
    for(auto& it : level->allHitbox())
    {
        if(hitbox.Intersects(it))
        {
            if(it != entity->hitbox)
                return true;
        }
    }

    return false;
}